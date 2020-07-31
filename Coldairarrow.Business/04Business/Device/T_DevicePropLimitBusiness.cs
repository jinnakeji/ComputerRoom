using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Device
{
    public class T_DevicePropLimitBusiness : BaseBusiness<T_DevicePropLimit>, IT_DevicePropLimitBusiness, IDependency
    {
        #region 外部接口

        public List<T_DevicePropLimit> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<T_DevicePropLimit>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<T_DevicePropLimit, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public List<V_DevicePropLimit> GetDataViewList(Pagination pagination, string condition, string keyword)
        {
            var q = Service.GetIQueryable<V_DevicePropLimit>();
            var where = LinqHelper.True<V_DevicePropLimit>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<V_DevicePropLimit, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public V_DevicePropLimit GetTheData(int propId)
        {
            return Service.GetIQueryable<V_DevicePropLimit>()
                .FirstOrDefault(x => x.PropId == propId);
        }

        public AjaxResult AddData(T_DevicePropLimit data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(T_DevicePropLimit data)
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