using Coldairarrow.Entity.MeterReaDing;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.MeterReaDing
{
    public class MeterReaDingTimeSetUpBusiness : BaseBusiness<MeterReaDingTimeSetUp>, IMeterReaDingTimeSetUpBusiness, IDependency
    {
        #region 外部接口

        public List<MeterReaDingTimeSetUp> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<MeterReaDingTimeSetUp>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<MeterReaDingTimeSetUp, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }
         
        public MeterReaDingTimeSetUp GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(MeterReaDingTimeSetUp data)
        {
            Insert(data);
           
            return Success();
        }

        public AjaxResult UpdateData(MeterReaDingTimeSetUp data)
        {
            Update(data); 
            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public MeterReaDingTimeSetUp GetListByUserId(string userid)
        {
            if (!userid.IsNullOrEmpty())
            {
                return GetIQueryable().Where(q => q.CreatorId == userid).FirstOrDefault();
            }
            return null;
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}