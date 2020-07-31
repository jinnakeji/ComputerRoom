using Coldairarrow.Entity.Hkv;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Hkv
{
    public class T_VideoBusiness : BaseBusiness<T_Video>, IT_VideoBusiness, IDependency
    {
        #region 外部接口

        public List<T_Video> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<T_Video>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<T_Video, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public T_Video GetTheData(int id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(T_Video data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(T_Video data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public List<T_Video> GetDataAll()
        {
            return GetIQueryable().ToList();
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}