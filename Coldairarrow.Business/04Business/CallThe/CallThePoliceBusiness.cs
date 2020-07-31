using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.CallThe;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Coldairarrow.Business.CallThe
{
    public class CallThePoliceBusiness : BaseBusiness<CallThePolice>, ICallThePoliceBusiness, IDependency
    {
        #region 外部接口

        public List<CallThePoliceDTO> GetDataList(Pagination pagination, string condition, string keyword)
        {
            //var q = GetIQueryable();
            //var where = LinqHelper.True<CallThePolice>();

            ////筛选
            //if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            //{
            //    var newWhere = DynamicExpressionParser.ParseLambda<CallThePolice, bool>(
            //        ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
            //    where = where.And(newWhere);
            //}

            //return q.Where(where).GetPagination(pagination).ToList();

            Expression<Func<CallThePolice, Base_Department, T_Device, CallThePoliceDTO>> select = (a, b, c) => new CallThePoliceDTO
            {
                DeviceName = c == null ? "" : c.Name,
                DepartmentName = b == null ? "" : b.Name 

            };
            select = select.BuildExtendSelectExpre();
            var q = from a in Service.GetIQueryable<CallThePolice>()
                    join b in Service.GetIQueryable<Base_Department>() on a.DepartmentId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    join e in Service.GetIQueryable<T_Device>() on a.DeviceInfoId equals e.Id.ToString() into ae
                    from e in ae.DefaultIfEmpty()
                    select @select.Invoke(a,b,e);
            var where = LinqHelper.True<CallThePoliceDTO>();
             
            if (!keyword.IsNullOrEmpty())
            {
                where = where.And(x =>
                    EF.Functions.Like(x.DeviceName, keyword)
                    || EF.Functions.Like(x.DepartmentName, keyword));
            } 
            var list = q.Where(where).GetPagination(pagination).ToList();
            return list;

        }

        public CallThePolice GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(CallThePolice data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(CallThePolice data)
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

    public class CallThePoliceDTO : CallThePolice 
    {
        public string DeviceName { get; set; }

        public string DepartmentName { get; set; }
    }
}