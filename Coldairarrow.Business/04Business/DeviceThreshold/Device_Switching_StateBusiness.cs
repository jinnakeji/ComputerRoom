using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.DeviceThreshold
{
    public class Device_Switching_StateBusiness : BaseBusiness<Device_Switching_State>, IDevice_Switching_StateBusiness, IDependency
    {
        #region 外部接口

        public List<Device_Switching_State> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Device_Switching_State>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Device_Switching_State, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public Device_Switching_State GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(Device_Switching_State data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(Device_Switching_State data)
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