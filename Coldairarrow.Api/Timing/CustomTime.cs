using Coldairarrow.Api.Hubs;
using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Business.Device;
using Coldairarrow.Business.MeterReaDing;
using Coldairarrow.Entity.Device;
using Coldairarrow.Entity.MeterReaDing;
using Coldairarrow.Util;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Timing
{
    public class CustomTime
    {
        ILogger logger { get; }
        IHubContext<RemoteHub> hubContext { get; }
        IMeterReaDingTimeSetUpBusiness timeSetUpBusiness { get; }
        IMeterReaDingOnDutyBusiness _meterReaDingOnDutyBus { get; }
        private IBase_DepartmentBusiness departmentBusiness { get; }
        private IDeviceDisplayModuleBusiness deviceDisplayModuleBusiness { get; }
        private List<MeterReaDingTimeSetUp> datas;
        int state = 0;
        public CustomTime(IHubContext<RemoteHub> hubContext,
            IMeterReaDingTimeSetUpBusiness timeSetUpBusiness, IMeterReaDingOnDutyBusiness meterReaDingOnDutyBus,
            ILogger logger, IBase_DepartmentBusiness departmentBusiness, IDeviceDisplayModuleBusiness deviceDisplayModuleBusiness)
        {
            this.hubContext = hubContext;
            this.timeSetUpBusiness = timeSetUpBusiness;
            this.logger = logger;
            _meterReaDingOnDutyBus = meterReaDingOnDutyBus;
            this.departmentBusiness = departmentBusiness;
            this.deviceDisplayModuleBusiness = deviceDisplayModuleBusiness;
        }

        public void Start()
        {
            this.datas = timeSetUpBusiness.GetList();
            new Thread(() =>
            {
                Thread.Sleep(1000);

                while (true)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        TimeSpan startTime = new TimeSpan(0, 0, 0);
                        TimeSpan endTime = new TimeSpan(0, 0, 0);
                        var go = datas.FirstOrDefault(item =>
                        {
                            startTime = ToTimeSpan(item.MeterTime);
                            endTime = ToTimeSpan(item.MeterTime).Add(TimeSpan.FromMinutes(int.Parse(item.RangeTime)));
                            var currentTime = ToTimeSpan(DateTime.Now.ToString("HH:mm"));
                            if (currentTime >= startTime && currentTime <= endTime)
                            {
                                return true;
                            }
                            return false;
                        });

                        if (go != null)
                        {
                            //时间范围内
                            hubContext.Clients.All.SendAsync("MeterReading", new { disabled = false, datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                            //程序抄表
                            if (CacheHelper.RedisCache.GetCache("state") != null && CacheHelper.RedisCache.GetCache("state").ToString() != "1")
                            {
                                MeterReading(go, startTime, endTime);
                            }
                        }
                        else
                        {
                            state = 0;
                            hubContext.Clients.All.SendAsync("MeterReading", new { disabled = true, datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }).Start();
        }

        public void UpdateData(List<MeterReaDingTimeSetUp> datas)
        {
            this.datas.Clear();
            this.datas.AddRange(datas);
        }

        private TimeSpan ToTimeSpan(string time)
        {
            var arr = time.Split(":");
            var span = new TimeSpan();

            if (arr.Length >= 1)
                span = span.Add(TimeSpan.FromHours(int.Parse(arr[0])));
            if (arr.Length >= 2)
                span = span.Add(TimeSpan.FromMinutes(int.Parse(arr[1])));
            if (arr.Length >= 3)
                span = span.Add(TimeSpan.FromSeconds(int.Parse(arr[2])));
            return span;
        }

        /// <summary>
        /// 抄表 
        /// </summary>
        /// <param name="go"></param>
        public void MeterReading(MeterReaDingTimeSetUp go, TimeSpan startTime, TimeSpan endTime)
        {
            var currentTime = ToTimeSpan(DateTime.Now.ToString("HH:mm"));
            if (currentTime == endTime && state == 0)
            {
                string[] arrlist = startTime.ToString().Split(":");
                string[] arrlistend = endTime.ToString().Split(":");
                //日期
                DateTime startdata = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(arrlist[0]), Convert.ToInt32(arrlist[1]), Convert.ToInt32(arrlist[2]));
                DateTime enddata = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(arrlistend[0]), Convert.ToInt32(arrlistend[1]), Convert.ToInt32(arrlistend[2]));

                //var startdata =  Convert.ToDateTime(DateTime.Now.Date.ToString() + startTime);
                //var enddata = Convert.ToDateTime(DateTime.Now.Date.ToString() + endTime);

                //部门
                //var data = departmentBusiness.GetList();
                //模块 设备
                var vdata = this.deviceDisplayModuleBusiness.GetVModeuleInfoList();
                var datas = CacheHelper.RedisCache.GetCache<List<T_DeviceData>>("DeviceData");

                var qMeter = from m in vdata
                             join b in datas on m.Departmentid equals b.DepartmentId
                             where (m.Deviceid == b.DeviceId && m.DeviceTypeId == b.DeviceTypeId && m.DevicePropId == b.DevicePropId)
                             select new
                             {
                                 Departmentid = m.Departmentid,
                                 DeviceDisplayModuleId = m.DeviceDisplayModuleId,
                                 moduleName = m.DeviceDisplayModuleName,
                                 DeviceName = m.DeviceName,
                                 propName = m.DevicePropName,
                                 DevicePropDisplayName = m.DevicePropDisplayName,
                                 propValue = b.Value,
                                 deviceTypeName = m.DeviceTypeName

                             };

                var deptidlist = from f in vdata group f.Departmentid by f.Departmentid;
                if (datas.Count > 0 && qMeter.Count() > 0)
                {
                    state = 1;

                    foreach (var item in deptidlist)
                    {
                        if (_meterReaDingOnDutyBus.GetMeterReaDingBydeptIdBystartTimeByendTime(item.Key.ToString(), startdata.ToString(), enddata.ToString()).Count == 0)
                        {
                            foreach (var list in qMeter.ToList())
                            {
                                if (list.propName != "updateTime")
                                {
                                    MeterReaDingOnDuty datainfo = new MeterReaDingOnDuty();

                                    datainfo.CreateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    datainfo.deptId = list.Departmentid;
                                    datainfo.DeviceDisplayModuleID = list.DeviceDisplayModuleId;
                                    datainfo.deviceName = list.DeviceName;
                                    datainfo.moduleName = list.moduleName;
                                    datainfo.propName = list.DevicePropDisplayName == null ? list.propName : list.DevicePropDisplayName;

                                    if (list.deviceTypeName == "aaNodeOnOff") //输出：0是关 -1是开  
                                    {
                                        datainfo.propValue = list.propValue == "0" ? "关" : list.propValue == "-1" ? "开" : list.propValue;
                                    }
                                    else if (list.deviceTypeName == "a5NodeOnOff") //输入：0报警  -1正常 
                                    {
                                        datainfo.propValue = list.propValue == "0" ? "报警" : list.propValue == "-1" ? "正常" : list.propValue;
                                    }
                                    else if (list.deviceTypeName == "airOnOff") //240是合闸，15是分闸
                                    {
                                        datainfo.propValue = list.propValue == "240" ? "合闸" : list.propValue == "15" ? "分闸" : list.propValue;
                                    }
                                    else
                                    {
                                        datainfo.propValue = list.propValue;
                                    }

                                    //datainfo.propValue = list.propValue;
                                    _meterReaDingOnDutyBus.AddData(datainfo);


                                }
                            }
                        }
                    }
                }

            }
        }
    }
}
