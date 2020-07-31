using Coldairarrow.Api.Hubs;
using Coldairarrow.Api.Models;
using Coldairarrow.Business.Device;
using Coldairarrow.Business.Enum;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Coldairarrow.Api.Controllers
{
    /// <summary>
    /// 远程调用
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class RemoteController : ControllerBase
    {
        const string DATAKEY = "DeviceData";
        const string DEVICE = "Device";
        const string DEVICEPROP = "DeviceProp";

        private static int Count;

        private ILogger logger;
        private IT_DeviceTypeBusiness _t_DeviceTypeBus { get; set; }
        private IT_DeviceBusiness _deviceBus { get; }
        private IT_DeviceDataBusiness _deviceDataBusiness { get; }
        private IT_DevicePropBusiness _devicePropBus { get; }
        private IT_TypeBusiness _typeBus { get; }
        private IDeviceDisplayModuleONdateBusiness deviceDisplayModuleONdate { get; }
        private IHubContext<RemoteHub> hubContext { get; }

        public RemoteController(ILogger logger,
            IT_DeviceBusiness deviceBus,
            IT_DeviceTypeBusiness t_DeviceTypeBus,
            IT_DeviceDataBusiness deviceDataBusiness,
            IT_TypeBusiness typeBus,
            IT_DevicePropBusiness devicePropBus,
            IHubContext<RemoteHub> hubContext,
            IDeviceDisplayModuleONdateBusiness deviceDisplayModuleONdate)
        {
            this.logger = logger;

            _deviceBus = deviceBus;
            _t_DeviceTypeBus = t_DeviceTypeBus;
            _deviceDataBusiness = deviceDataBusiness;
            _typeBus = typeBus;
            _devicePropBus = devicePropBus;
            this.deviceDisplayModuleONdate = deviceDisplayModuleONdate;
            this.hubContext = hubContext;
        }

        #region 公有方法

        /// <summary>
        /// 设备数据结构
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<object>> DeviceInfo()
        {
            StreamReader sr = new StreamReader(this.Request.Body, Encoding.UTF8);
            var text = System.Web.HttpUtility.UrlDecode(sr.ReadToEnd().Trim()).Replace("data", "").Replace("=", "");
            logger.Info(LogType.系统跟踪, "收到设备信息---DeviceInfo:" + text);
            try
            {
                text = ConvertAirNumber(text);
                DeviceDataInfoStr devicelsit = JsonConvert.DeserializeObject<DeviceDataInfoStr>(text);
                if (!string.IsNullOrWhiteSpace(devicelsit.deviceid))
                {
                    Pagination pagination = new Pagination();
                    var _iV_DeviceList = _deviceBus.GetDataViewList(pagination, null, null);

                    var deviceTypeList = _t_DeviceTypeBus.GetDataList();
                    List<T_Device> t_Devices = new List<T_Device>();

                    int number = 0;
                    foreach (var item in devicelsit.deviceInfo)
                    {
                        if (item != null)
                        {
                            int.TryParse(item.number, out number);
                            var vDevice = _iV_DeviceList.FirstOrDefault(t => t.DeviceCode == devicelsit.deviceid && t.DeviceTypeName == item.deviceType && t.Number == number);
                            if (vDevice == null)
                            {
                                T_Device device = new T_Device();
                                device.Name = item.name;
                                device.Number = number;
                                device.DeviceCode = devicelsit.deviceid;
                                device.DeviceMacCode = devicelsit.mac;

                                var deviceType = deviceTypeList.ToList().FirstOrDefault(o => o.Name == item.deviceType); //_t_DeviceTypeBus.GetByTypeName(item.deviceType);
                                if (deviceType == null)
                                {
                                    deviceType = new T_DeviceType();
                                    deviceType.Name = item.deviceType;
                                    switch (item.deviceType.Trim())
                                    {
                                        case "AAAirNodeOnOff":
                                            deviceType.DisplayName = "空调开关";
                                            break;
                                        case "Angel":
                                            deviceType.DisplayName = "倾斜角";
                                            break;
                                        case "Battery":
                                            deviceType.DisplayName = "电池巡检";
                                            break;
                                        case "NodeTemperature":
                                            deviceType.DisplayName = "模拟量输入";
                                            break;
                                        case "CO2":
                                            deviceType.DisplayName = "有害气体";
                                            break;
                                        case "GroundResistance":
                                            deviceType.DisplayName = "接地电阻";
                                            break;
                                        case "NodeTempAndHumidity":
                                            deviceType.DisplayName = "温湿度";
                                            break;
                                        case "A5NodeOnOff":
                                            deviceType.DisplayName = "开关量输入";
                                            break;
                                        case "AirOnOff":
                                            deviceType.DisplayName = "空气开关";
                                            break;

                                        case "AANodeOnOff":
                                            deviceType.DisplayName = "开关量输出";
                                            break;

                                        case "ThreeElec":
                                            deviceType.DisplayName = "三相电检测";
                                            break;
                                    }

                                    _t_DeviceTypeBus.AddData(deviceType);
                                }

                                device.DeviceTypeId = deviceType.Id;

                                t_Devices.Add(device);
                            }
                        }
                    }

                    _deviceBus.AddData(t_Devices);
                }
                else
                {
                    logger.Info(LogType.系统跟踪, " 设备信息不全，deviceid地址不存在");
                }
            }
            catch (Exception ex)
            {
                //logger.Info(LogType.系统异常, " 设备信息解析保存错误: " + ex.Message);
                logger.Error(LogType.系统异常, " 设备信息解析保存错误: " + ex.Message);
                logger.Info(LogType.系统跟踪, "DeviceInfo:" + text);
                throw;
            }

            return new JsonResult(new
            {
                success = true
            });
        }

        /// <summary>
        /// 设备数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<object>> DeviceData()
        {
            Count++;
            StreamReader sr = new StreamReader(this.Request.Body, Encoding.UTF8);
            var text = System.Web.HttpUtility.UrlDecode(sr.ReadToEnd().Trim()).Replace("data", "").Replace("=", "");
            // logger.Info(LogType.系统跟踪, "DeviceData:" + text); 
            try
            {
                //  logger.Debug(LogType.系统跟踪, Newtonsoft.Json.Linq.JObject.Parse(text).ToString());
                var datas = ConvertDeviceStruct(text, out string deviceId, out string mac);
                string deviceid = text.ToJObject()["deviceid"]?.ToString();
                if (string.IsNullOrWhiteSpace(deviceid))
                {
                    logger.Info(LogType.系统跟踪, " 设备deviceid地址不存在 ");
                }
                else
                {
                    var deviceDatas = ConvertDeviceData(datas, deviceid, out string error);
                    string departmentId = GetDepartmentId(deviceid);
                    if (!string.IsNullOrWhiteSpace(departmentId))
                    {
                        SendData(deviceDatas, departmentId);
                        if (Count >= 10)
                        {
                            Count = 0;
                            AddSaveDatas(deviceDatas);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogType.系统异常, " 设备数据解析保存错误: " + ex.Message);
                logger.Error(LogType.系统异常, ex.StackTrace);
                logger.Info(LogType.系统跟踪, "DeviceData:" + text);
                throw ex;
            }

            return new JsonResult(new
            {
                success = true
            });
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 转换空气开关方法
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string ConvertAirNumber(string json)
        {
            JObject jObject = JObject.Parse(json);

            var jarrry = jObject["deviceinfo"].ToArray();

            foreach (var item in jarrry)
            {
                if (item["deviceType"]?.ToString() == "airCondition")
                {
                    item["number"] = item["airNumber"];
                }
            }
            return jObject.ToString();
        }

        /// <summary>
        /// 转化数据结构方法
        /// </summary>
        /// <param name="json"></param>
        /// <param name="deviceId"></param>
        /// <param name="mac"></param>
        /// <returns></returns>
        private List<RemoteModel> ConvertDeviceStruct(string json, out string deviceId, out string mac)
        {
            JObject jObject = JObject.Parse(json);
            var datas = new List<RemoteModel>();
            foreach (var item in jObject)
            {
                var model = new RemoteModel();
                datas.Add(model);
                model.DeviceType = item.Key;
                item.Value.ToArray().ForEach(aa =>
                {
                    var device = new Coldairarrow.Api.Models.Device()
                    {
                        NodeNumber = Convert.ToInt32(aa["nodeNumber"]),
                        TimeStamp = Convert.ToInt64(aa["timeStamp"])
                    };
                    model.DeviceList.Add(device);
                    foreach (JProperty b in aa)
                    {
                        if (b.Name == "nodeNumber" || b.Name == "timeStamp" || b.Name == "deviceStatusEnum")
                            continue;
                        device[b.Name] = b.Value.ToString();
                    }
                });
            }
            deviceId = jObject["deviceid"]?.ToString();
            mac = jObject["mac"]?.ToString();
            return datas;
        }

        /// <summary>
        /// 添加并且保存数据
        /// </summary>
        /// <param name="deviceDatas"></param>
        private void AddSaveDatas(List<T_DeviceData> deviceDatas)
        {
            var datas = CacheHelper.RedisCache.GetCache<List<T_DeviceData>>(DATAKEY);
            if (datas != null)
                deviceDatas.AddRange(datas);

            if (deviceDatas.Count >= 1000)
            {
                CacheHelper.RedisCache.RemoveCache(DATAKEY);
                _deviceDataBusiness.BulkInsert(deviceDatas);
            }
            else
            {
                CacheHelper.RedisCache.SetCache(DATAKEY, deviceDatas);
            }
        }

        /// <summary>
        /// 获取部门Id
        /// </summary>
        /// <returns></returns>
        private string GetDepartmentId(string deviceId)
        {
            var deviceList = _deviceBus.GetList();
            var departmentList = deviceDisplayModuleONdate.GetList();

            var dept = (from device in deviceList
                        join department in departmentList
                        on device.Id equals department.DeviceId
                        where device.DeviceCode == deviceId
                        select department).FirstOrDefault();

            return dept?.DepartmentId;
        }

        /// <summary>
        /// 推送数据
        /// </summary>
        private void SendData(List<T_DeviceData> datas, string departmentId)
        {
            hubContext.Clients.All.SendAsync(nameof(DeviceData), datas, departmentId);
        }

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="deviceid"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private List<T_DeviceData> ConvertDeviceData(List<RemoteModel> datas, string deviceid, out string error)
        {
            var datatype = DataTypeEnum.Enum.@string.ToString();
            DateTime dateTime = DateTime.Now;
            List<T_DeviceData> t_DeviceDatas = new List<T_DeviceData>();
            var deviceList = CacheHelper.RedisCache.GetCache<List<V_Device>>(DEVICE);
            var deviceprops = CacheHelper.RedisCache.GetCache<List<T_DeviceProp>>(DEVICEPROP);

            if(true) //(deviceList == null)
            {
                deviceList = _deviceBus.GetDataViewList(new Pagination(), null, null);
                CacheHelper.RedisCache.SetCache(DEVICE, deviceList);
            }
            if(true) //(deviceprops == null)
            {
                deviceprops = _devicePropBus.GetList();

                foreach (var item in deviceprops)
                {
                    switch (item.Name)
                    {

                        /// <summary>
                        /// 温度  ℃
                        /// </summary>
                        case "temperature":
                            item.DisplayName = "温度";
                            item.Unit = "℃";
                            break;
                        /// <summary>
                        /// 温度原始值 ℃
                        /// </summary>
                        case " temperatureOriginal":
                            item.DisplayName = "温度原始值";
                            item.Unit = "℃";
                            break;
                        /// <summary>
                        /// X轴角度 °
                        /// </summary>
                        case "xAngel":
                            item.DisplayName = "X轴角度";
                            item.Unit = "°";
                            break;
                        /// <summary>
                        ///  X轴角度原始值  °
                        /// </summary>
                        case "xAngelOriginal":
                            item.DisplayName = "X轴角度原始值";
                            item.Unit = "°";
                            break;
                        /// <summary>
                        /// Y轴角度原始值   °
                        /// </summary>
                        case "yAngelOriginal":
                            item.DisplayName = "Y轴角度原始值";
                            item.Unit = "°";
                            break;
                        /// <summary>
                        /// 电压  V
                        /// </summary>
                        case "voltage":
                            item.DisplayName = "电压";
                            item.Unit = "V";
                            break;
                        /// <summary>
                        /// 电阻值 mΩ
                        /// </summary>
                        case "resistance":
                            item.DisplayName = "电阻值";
                            item.Unit = "mΩ";
                            break;
                        /// <summary>
                        /// 电流  A
                        /// </summary>
                        case "current":
                            item.DisplayName = "电流";
                            item.Unit = "A";
                            break;
                        /// <summary>
                        /// 总电压 V
                        /// </summary>
                        case "voltageTotal":
                            item.DisplayName = "总电压";
                            item.Unit = "V";
                            break;
                        /// <summary>
                        /// 含量  ppm
                        /// </summary>
                        case "value":
                            item.DisplayName = "含量";
                            item.Unit = "ppm";
                            break;
                        /// <summary>
                        /// 湿度  ％
                        /// </summary>
                        case "humidity":
                            item.DisplayName = "湿度";
                            item.Unit = "％";
                            break;
                        /// <summary>
                        /// 开关状态
                        /// </summary>
                        case "onOff":
                            item.DisplayName = "开关状态";
                            break;
                        /// <summary>
                        /// 频率  Hz
                        /// </summary>
                        case "freq":
                            item.DisplayName = "频率";
                            item.Unit = "Hz";
                            break;
                        /// <summary>
                        /// 有功功率 W
                        /// </summary>
                        case "power":
                            item.DisplayName = "有功功率";
                            item.Unit = "W";
                            break;
                        /// <summary>
                        /// 功率因数 cos
                        /// </summary>
                        case "powerNumber":
                            item.DisplayName = "功率因数";
                            item.Unit = "cos";
                            break;
                        /// <summary>
                        /// 无功功率 Var
                        /// </summary>
                        case "qpower":
                            item.DisplayName = "无功功率";
                            item.Unit = "Var";
                            break;
                        /// <summary>
                        /// 视在功率  Var
                        /// </summary>
                        case "spower":
                            item.DisplayName = "视在功率";
                            item.Unit = "Var";
                            break;
                        /// <summary>
                        /// 线电压  V
                        /// </summary>
                        case "voltageLine":
                            item.DisplayName = "线电压";
                            item.Unit = "V";
                            break;
                        /// <summary>
                        /// 实时漏电 mA
                        /// </summary>
                        case "currentLeak":
                            item.DisplayName = "实时漏电";
                            item.Unit = "mA";
                            break;
                        /// <summary>
                        /// 电度计量 度
                        /// </summary>
                        case "elecDegree":
                            item.DisplayName = "电度计量";
                            item.Unit = "度";
                            break;

                        case "updateTime":
                            item.DisplayName = "更新时间";
                            break;

                        default:
                            break;
                    }
                }

                CacheHelper.RedisCache.SetCache(DEVICEPROP, deviceprops);
            }

            int typeId = _typeBus.GetList().FirstOrDefault(t => t.Name == datatype).Id;

            List<string> result = new List<string>();
            foreach (var d in datas)
            {
                foreach (var v in d.DeviceList)
                {
                    var device = deviceList.FirstOrDefault(t => t.DeviceCode == deviceid && t.DeviceTypeName.Trim().ToLower() == d.DeviceType.Trim().ToLower() && t.Number == v.NodeNumber);
                    if (device != null)
                    {
                        foreach (KeyValuePair<string, string> item in v)
                        {

                            var t_DeviceData = new T_DeviceData();
                            t_DeviceData.DepartmentId = _deviceBus.GetDepartmentId(device.Id);

                            if (string.IsNullOrWhiteSpace(t_DeviceData.DepartmentId))
                                continue;

                            var prop = deviceprops.FirstOrDefault(t => t.DeviceTypeId == device.DeviceTypeId && t.Name.ToLower().Trim() == item.Key.ToLower().Trim());
                            if (prop == null)
                            {
                                prop = new T_DeviceProp();
                                prop.Name = item.Key;
                                prop.TypeId = typeId;
                                prop.DeviceTypeId = device.DeviceTypeId;
                                _devicePropBus.AddData(prop);
                                deviceprops = _devicePropBus.GetList();
                                CacheHelper.RedisCache.SetCache(DEVICEPROP, deviceprops);
                            }
                            t_DeviceData.DevicePropId = prop.Id;

                            t_DeviceData.DeviceId = device.Id;
                            t_DeviceData.DeviceTypeId = device.DeviceTypeId;
                            t_DeviceData.Value = item.Value?.Trim();
                            t_DeviceData.CreateTime = dateTime;
                            t_DeviceDatas.Add(t_DeviceData);
                        }
                    }
                    else
                    {
                        result.Append("{" + "deviceid:" + deviceid + ",DeviceType:" + d.DeviceType + ",Number:" + v.NodeNumber + "}");
                    }
                }
            }


            if (result.Count > 0)
            {
                error = $"[{string.Join(",", result)}]";
            }
            else
                error = null;

            return t_DeviceDatas;
        }

        #endregion
    }

    public class DeviceDataInfoStr
    {
        public string mac { get; set; }
        public string deviceid { get; set; }
        public List<Coldairarrow.Entity.Remote.DeviceInfo> deviceInfo { get; set; }
    }
    public enum EmployeeType
    {
        /// <summary>
        /// 温度  ℃
        /// </summary>
        temperature,
        /// <summary>
        /// 温度原始值 ℃
        /// </summary>
        temperatureOriginal,
        /// <summary>
        /// X轴角度 °
        /// </summary>
        xAngel,
        /// <summary>
        ///  X轴角度原始值  °
        /// </summary>
        xAngelOriginal,
        /// <summary>
        /// Y轴角度原始值   °
        /// </summary>
        yAngelOriginal,
        /// <summary>
        /// 电压  V
        /// </summary>
        voltage,
        /// <summary>
        /// 电阻值 mΩ
        /// </summary>
        resistance,
        /// <summary>
        /// 电流  A
        /// </summary>
        current,
        /// <summary>
        /// 总电压 V
        /// </summary>
        voltageTotal,
        /// <summary>
        /// 含量  ppm
        /// </summary>
        value,
        /// <summary>
        /// 湿度  ％
        /// </summary>
        humidity,
        /// <summary>
        /// 开关状态
        /// </summary>
        onOff,
        /// <summary>
        /// 频率  Hz
        /// </summary>
        freq,
        /// <summary>
        /// 有功功率 W
        /// </summary>
        power,
        /// <summary>
        /// 功率因数 cos
        /// </summary>
        powerNumber,
        /// <summary>
        /// 无功功率 Var
        /// </summary>
        qpower,
        /// <summary>
        /// 视在功率  Var
        /// </summary>
        spower,
        /// <summary>
        /// 线电压  V
        /// </summary>
        voltageLine,
        /// <summary>
        /// 实时漏电 mA
        /// </summary>
        currentLeak,
        /// <summary>
        /// 电度计量 度
        /// </summary>
        elecDegree


    }
}