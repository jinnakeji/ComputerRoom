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
    public class A5NodeOnOffBusiness : BaseBusiness<A5NodeOnOff>, IA5NodeOnOffBusiness, IDependency
    {
        #region 外部接口

        //public List<A5NodeOnOff> GetDataList(Pagination pagination, string condition, string keyword)
        //{
        //    var q = GetIQueryable();
        //    var where = LinqHelper.True<A5NodeOnOff>();

        //    //筛选
        //    if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
        //    {
        //        var newWhere = DynamicExpressionParser.ParseLambda<A5NodeOnOff, bool>(
        //            ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
        //        where = where.And(newWhere);
        //    }

        //    return q.Where(where).GetPagination(pagination).ToList();
        //}

        //public A5NodeOnOff GetTheData(string id)
        //{
        //    return GetEntity(id);
        //}
        public List<A5NodeOnOffDTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null)
        { 
            Expression<Func<A5NodeOnOff, DeviceInfo, Base_Department, A5NodeOnOffDTO>> select = (a, b, c) => new A5NodeOnOffDTO
            {
                DepartmentName = c.Name,
                DeviceName = b.DeviceName,
                NodeNumber = b.DeviceNode
            };
            select = select.BuildExtendSelectExpre();
            var q_NodeTempAndHumidity = all ? Service.GetIQueryable<A5NodeOnOff>() : GetIQueryable();
            var q = from a in q_NodeTempAndHumidity.AsExpandable()
                    join b in Service.GetIQueryable<DeviceInfo>() on new { nodeNumber = a.nodeNumber, devicetype = "A5NodeOnOff" } equals new { nodeNumber = b.DeviceNode, devicetype = b.deviceType } into ab
                    from b in ab.DefaultIfEmpty()
                    join c in Service.GetIQueryable<Base_Department>() on ParentId equals c.Id into ac
                    from c in ac.DefaultIfEmpty()
                    select @select.Invoke(a, b, c);

            var where = LinqHelper.True<A5NodeOnOffDTO>();
            //if (!userId.IsNullOrEmpty())
            //    where = where.And(x => x.Id == userId);
            var list = q.Where(where).GetPagination(pagination).ToList();

            return list;
        }

        public A5NodeOnOffDTO GetTheData(string id)
        {
            if (id.IsNullOrEmpty())
                return null;
            else
                return GetDataList(new Pagination(), true, id).FirstOrDefault();
        }
        public AjaxResult AddData(A5NodeOnOff data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(A5NodeOnOff data)
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

    public class A5NodeOnOffDTO : A5NodeOnOff
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