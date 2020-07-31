using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Coldairarrow.Business.Device
{
    public class DeviceInfoBusiness : BaseBusiness<DeviceInfo>, IDeviceInfoBusiness, IDependency
    {

        #region 外部接口 
        public List<DeviceInfoDTO> GetDataList(Pagination pagination, bool all, string userId = null, string keyword = null)
        {
            Expression<Func<DeviceInfo, Base_Department, DeviceDisplayModule, DeviceInfoDTO>> select = (a, b, c) => new DeviceInfoDTO
            {
                DepartmentName = b.Name,
                DeviceDisplayModuleName = c.DeviceDisplayModuleName
            };
            select = select.BuildExtendSelectExpre();
            var q_DeviceInfo = all ? Service.GetIQueryable<DeviceInfo>() : GetIQueryable();
            var q = from a in q_DeviceInfo.AsExpandable()
                    join b in Service.GetIQueryable<Base_Department>() on a.DepartmentId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    join c in Service.GetIQueryable<DeviceDisplayModule>() on a.DeviceDisplayModuleId equals c.Id into ac
                    from c in ac.DefaultIfEmpty()
                    select @select.Invoke(a, b, c);


            var where = LinqHelper.True<DeviceInfoDTO>();
            if (!userId.IsNullOrEmpty())
                where = where.And(x => x.Id == userId);
            if (!keyword.IsNullOrEmpty())
            {
                where = where.And(x =>
                    EF.Functions.Like(x.DeviceName, keyword)
                    || EF.Functions.Like(x.DeviceNode, keyword));
            }
            var list = q.Where(where).GetPagination(pagination).ToList();

            return list;
        }

        public DeviceInfoDTO GetTheData(string id)
        {
            if (id.IsNullOrEmpty())
                return null;
            else
                return GetDataList(new Pagination(), true, id).FirstOrDefault();
        }

        public List<DeviceInfo> GetControlDeviceInfo() 
        {
            string strsql = @"select *from  [DeviceInfo] where isControl=1";
            var qlist= Service.GetListBySql<DeviceInfo>(strsql);
            return qlist;
        }        
        public AjaxResult AddData(DeviceInfo data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(DeviceInfo data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        public List<string> GetDeviceInfoDataList(string id)
        {
            List<string> datalist = new List<string>();

            string strline = "<div class=\"line\">{0}</div>";
            string strlabel = "<label>{0}</label>";
            string voltagestrfield = "<div class=\"field\"><span class=\"name\">A:</span> <span class=\"value\">{0}V</span></div>" +
                                     "<div class=\"field\"><span class=\"name\">B:</span> <span class=\"value\">{1}V</span></div>" +
                                     "<div class=\"field\"><span class=\"name\">C:</span> <span class=\"value\">{2}V</span></div>";
            string onofffield = "<div class=\"field\"><span class=\"name\">{0}:</span> <span class=\"value\">{1}</span></div>";
            //< div class="line">
            //   <label>主电监测</label>
            //   <div class="field">
            //     <span class="name">A:</span>
            //     <span class="value">220V</span>
            //   </div>
            //   <div class="field">
            //     <span class="name">B:</span>
            //     <span class="value">220V</span>
            //   </div>
            //   <div class="field">
            //     <span class="name">C:</span>
            //     <span class="value">220V</span>
            //   </div>
            // </div>

            List<DeviceInfo> dinfolist = Service.GetIQueryable<DeviceInfo>().Where(x => x.DeviceDisplayModuleId == id).ToList();
            foreach (var item in dinfolist)
            {
                string strsql = string.Format("select top 1 * from {0}  where nodeNumber={1}  order by updatetime desc", item.deviceType, item.DeviceNode).ToString();
                DataTable dtb_xx = Service.GetDataTableWithSql(strsql);
                string dname = string.Format(strlabel, item.DeviceName);
                if (dtb_xx.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtb_xx.Rows)
                    {
                        if (dtb_xx.Columns.Contains("onoff"))
                        {

                            //  dname += ":" + dr["onoff"].ToString();
                            dname += string.Format(onofffield, "状态", dr["onoff"].ToString());
                        }
                        //电压【A、B、C】
                        if (dtb_xx.Columns.Contains("voltagestr"))
                        {
                            //   dname += ":" + dr["voltagestr"].ToString().Split(',');
                            dname += string.Format(voltagestrfield, dr["voltagestr"].ToString().Split(',')[0], dr["voltagestr"].ToString().Split(',')[1], dr["voltagestr"].ToString().Split(',')[2]);
                        }
                        //零电流
                        if (dtb_xx.Columns.Contains("zeroCurrent"))
                        {
                            dname += string.Format(onofffield, "零电流", dr["zeroCurrent"].ToString());
                        }
                        //电流 ‘【A、B、C】
                        if (dtb_xx.Columns.Contains("currentStr"))
                        {
                            dname += string.Format(voltagestrfield, dr["currentStr"].ToString().Split(',')[0], dr["currentStr"].ToString().Split(',')[1], dr["currentStr"].ToString().Split(',')[2]);

                        }
                        //正常温度
                        if (dtb_xx.Columns.Contains("temperature"))
                        {

                            dname += string.Format(onofffield, "温度", dr["temperature"].ToString());
                        }
                        //正常湿度
                        if (dtb_xx.Columns.Contains("humidity"))
                        {
                            // dr["humidity"].ToString();
                            dname += string.Format(onofffield, "湿度", dr["humidity"].ToString());
                        }
                        //电阻
                        if (dtb_xx.Columns.Contains("resistance"))
                        {
                            //  dr["resistance"].ToString();
                            dname += string.Format(onofffield, "电阻", dr["resistance"].ToString());
                        }
                        //电压
                        if (dtb_xx.Columns.Contains("voltage"))
                        {
                            //dr["voltage"].ToString();
                            dname += string.Format(voltagestrfield, dr["voltage"].ToString().Split(',')[0], dr["voltage"].ToString().Split(',')[1], dr["voltage"].ToString().Split(',')[2]);
                        }
                        //电流
                        if (dtb_xx.Columns.Contains("current"))
                        {
                            //dr["current"].ToString();
                            dname += string.Format(onofffield, "电流", dr["current"].ToString());
                        }
                        // 二氧化碳
                        if (dtb_xx.Columns.Contains("value"))
                        {
                            //dr["value"].ToString();
                            dname += string.Format(onofffield, "二氧化碳", dr["value"].ToString());
                        }
                        //总功率
                        if (dtb_xx.Columns.Contains("totalPower"))
                        {
                            //dr["totalPower"].ToString();
                            dname += string.Format(onofffield, "总功率", dr["totalPower"].ToString());
                        }
                    }
                }
                else
                {
                    dname += string.Format(onofffield, "暂无数据", "设备未接入");
                }
                datalist.Add(string.Format(strline, dname).ToString());


            }





            return datalist;
        }

       
        #endregion

    }
    public class DeviceInfoDTO : DeviceInfo
    {
        public string DeviceDisplayModuleName { get; set; }

        public string DepartmentName { get; set; }
    }
}