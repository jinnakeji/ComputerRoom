using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.MeterReaDing;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Coldairarrow.Business.MeterReaDing
{
    public class ChangeShiftsBusiness : BaseBusiness<ChangeShifts>, IChangeShiftsBusiness, IDependency
    {
        #region 外部接口

        public List<ChangeShiftsDTO> GetDataList(Pagination pagination, string condition, string keyword)
        {
            //var q = GetIQueryable();
            //var where = LinqHelper.True<ChangeShifts>();

            ////筛选
            //if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            //{
            //    var newWhere = DynamicExpressionParser.ParseLambda<ChangeShifts, bool>(
            //        ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
            //    where = where.And(newWhere);
            //}

            //return q.Where(where).GetPagination(pagination).ToList();
            Expression<Func<ChangeShifts, Base_User, Base_User, TeamTable, TeamTable, ChangeShiftsDTO>> select = (a, b, c ,e,f) => new ChangeShiftsDTO
            {
                UserName = b != null ? b.UserName : "",
                ChangeUserName = c != null ? c.UserName : "",
                UserTemp = e != null ? e.TeamTableName : "",
                ChangeUserTemp = f != null ? f.TeamTableName : "",
                CreateTime = a.CreateTime,

            };
            select = select.BuildExtendSelectExpre();
            var q = from a in Service.GetIQueryable<ChangeShifts>()
                    join b in Service.GetIQueryable<Base_User>() on a.UserId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    join e in Service.GetIQueryable<Base_User>() on a.ChangeUserId equals e.Id into ae 
                    from e in ae.DefaultIfEmpty()
                    join bb in Service.GetIQueryable<TeamTable>() on b.TeamTableId equals bb.Id into aab
                    from abb in aab.DefaultIfEmpty()
                    join ee in  Service.GetIQueryable<TeamTable>() on e.TeamTableId equals ee.Id into aae
                    from aee in aae.DefaultIfEmpty()
                    select @select.Invoke(a, b, e,abb,aee);
            var where = LinqHelper.True<ChangeShiftsDTO>();
            if (!keyword.IsNullOrEmpty())
            {
                where = where.And(x =>
                    EF.Functions.Like(x.UserName, keyword)
                    || EF.Functions.Like(x.ChangeUserName, keyword));
            }
            var list = q.Where(where).GetPagination(pagination).ToList();

            return list;
        }

        public ChangeShifts GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(ChangeShifts data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(ChangeShifts data)
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
    public class ChangeShiftsDTO : ChangeShifts
    {
        /// <summary>
        /// 交接人员
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 交接班组
        /// </summary>
        public string UserTemp { get; set; }
        /// <summary>
        /// 接班人员
        /// </summary>
        public string ChangeUserName { get; set; }
        /// <summary>
        /// 接班班组
        /// </summary>
        public string ChangeUserTemp { get; set; }

        public string CreateTimeText { get => CreateTime.ToString("yyyy-MM-dd"); }
    }
}