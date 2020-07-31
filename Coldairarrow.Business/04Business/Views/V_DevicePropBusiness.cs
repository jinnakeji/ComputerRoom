using Coldairarrow.Entity.Views;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Views
{
    public class V_DevicePropBusiness : BaseBusiness<V_DeviceProp>, IV_DevicePropBusiness, IDependency
    {
        #region 外部接口

        public List<V_DeviceProp> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<V_DeviceProp>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<V_DeviceProp, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public V_DeviceProp GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(V_DeviceProp data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(V_DeviceProp data)
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