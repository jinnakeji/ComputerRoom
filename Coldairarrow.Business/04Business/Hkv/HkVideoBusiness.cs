using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.Hkv;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Coldairarrow.Business.Hkv
{
    public class HkVideoBusiness : BaseBusiness<HkVideo>, IHkVideoBusiness, IDependency
    {
        #region 外部接口

        //public List<HkVideo> GetDataList(Pagination pagination, string condition, string keyword)
        //{
        //    var q = GetIQueryable();
        //    var where = LinqHelper.True<HkVideo>();

        //    //筛选
        //    if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
        //    {
        //        var newWhere = DynamicExpressionParser.ParseLambda<HkVideo, bool>(
        //            ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
        //        where = where.And(newWhere);
        //    }

        //    return q.Where(where).GetPagination(pagination).ToList();
        //}
        public List<HkVideoDTO> GetDataList(Pagination pagination, string condition, string keyword)
        {
            Expression<Func<HkVideo, Base_Department, HkVideoDTO>> select = (a, b) => new HkVideoDTO
            {
                DepartmentName = b.Name 
            };
            bool all = false;
            select = select.BuildExtendSelectExpre();
            var q_DeviceInfo = all ? Service.GetIQueryable<HkVideo>() : GetIQueryable();
            var q = from a in q_DeviceInfo.AsExpandable()
                    join b in Service.GetIQueryable<Base_Department>() on a.DepartmentId equals b.Id into ab
                    from b in ab.DefaultIfEmpty() 
                    select @select.Invoke(a, b); 
            var where = LinqHelper.True<HkVideoDTO>(); 
            if (!keyword.IsNullOrEmpty())
            {
                where = where.And(x =>
                    EF.Functions.Like(x.Ip, keyword)
                    || EF.Functions.Like(x.ViodeName, keyword));
            }
            var list = q.Where(where).GetPagination(pagination).ToList();

            return list;
        }

        public HkVideo GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(HkVideo data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(HkVideo data)
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

    public class HkVideoDTO : HkVideo
    {
        public string DepartmentName { get; set; }
    }
}