using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.DeviceThreshold
{
    public class Three_Electric_ThresholdBusiness : BaseBusiness<Three_Electric_Threshold>, IThree_Electric_ThresholdBusiness, IDependency
    {
        #region 外部接口

        public List<Three_Electric_Threshold> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Three_Electric_Threshold>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Three_Electric_Threshold, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public Three_Electric_Threshold GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(Three_Electric_Threshold data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(Three_Electric_Threshold data)
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