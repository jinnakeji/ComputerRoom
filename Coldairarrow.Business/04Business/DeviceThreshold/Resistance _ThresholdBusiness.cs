using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.DeviceThreshold
{
    public class Resistance_ThresholdBusiness : BaseBusiness<Resistance_Threshold>, IResistance_ThresholdBusiness, IDependency
    {
        #region 外部接口

        public List<Resistance_Threshold> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Resistance_Threshold>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Resistance_Threshold, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public Resistance_Threshold GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(Resistance_Threshold data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(Resistance_Threshold data)
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