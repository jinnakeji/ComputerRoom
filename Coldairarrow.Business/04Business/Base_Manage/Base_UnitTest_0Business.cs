using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_UnitTest_0Business : BaseBusiness<Base_UnitTest_0>, IBase_UnitTest_0Business, IDependency
    {
        #region 外部接口

        public List<Base_UnitTest_0> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Base_UnitTest_0>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Base_UnitTest_0, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public Base_UnitTest_0 GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(Base_UnitTest_0 data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(Base_UnitTest_0 data)
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