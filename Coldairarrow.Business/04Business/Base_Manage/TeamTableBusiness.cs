using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Base_Manage
{
    public class TeamTableBusiness : BaseBusiness<TeamTable>, ITeamTableBusiness, IDependency
    {
        #region 外部接口

        public List<TeamTable> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<TeamTable>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<TeamTable, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public TeamTable GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(TeamTable data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(TeamTable data)
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
}