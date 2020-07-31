using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Coldairarrow.Business.DataManage
{
    public class CO2Business : BaseBusiness<CO2>, ICO2Business, IDependency
    {
        #region 外部接口

        //public List<CO2> GetDataList(Pagination pagination, string condition, string keyword)
        //{
        //    var q = GetIQueryable();
        //    var where = LinqHelper.True<CO2>();

        //    //筛选
        //    if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
        //    {
        //        var newWhere = DynamicExpressionParser.ParseLambda<CO2, bool>(
        //            ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
        //        where = where.And(newWhere);
        //    }

        //    return q.Where(where).GetPagination(pagination).ToList();
        //}

        //public CO2 GetTheData(string id)
        //{
        //    return GetEntity(id);
        //}
        public List<CO2DTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null)
        {
            Expression<Func<CO2, DeviceInfo, Base_Department, CO2DTO>> select = (a, b, c) => new CO2DTO
            {
                DepartmentName = c.Name,
                DeviceName = b.DeviceName,
                NodeNumber = b.DeviceNode
            };
            select = select.BuildExtendSelectExpre();
            var q_NodeTempAndHumidity = all ? Service.GetIQueryable<CO2>() : GetIQueryable();
            var q = from a in q_NodeTempAndHumidity.AsExpandable()
                    join b in Service.GetIQueryable<DeviceInfo>() on new { nodeNumber = a.nodeNumber, devicetype = "CO2" } equals new { nodeNumber = b.DeviceNode, devicetype = b.deviceType } into ab
                    from b in ab.DefaultIfEmpty()
                    join c in Service.GetIQueryable<Base_Department>() on ParentId equals c.Id into ac
                    from c in ac.DefaultIfEmpty()
                    select @select.Invoke(a, b, c);

            var where = LinqHelper.True<CO2DTO>();
            //if (!userId.IsNullOrEmpty())
            //    where = where.And(x => x.Id == userId);
            var list = q.Where(where).GetPagination(pagination).ToList();

            return list;
        }

        public CO2DTO GetTheData(string id)
        {
            if (id.IsNullOrEmpty())
                return null;
            else
                return GetDataList(new Pagination(), true, id).FirstOrDefault();
        }
        public AjaxResult AddData(CO2 data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(CO2 data)
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

        #endregion
    }
    public class CO2DTO : CO2
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 节点号
        /// </summary>
        public string NodeNumber { get; set; }

    }
}