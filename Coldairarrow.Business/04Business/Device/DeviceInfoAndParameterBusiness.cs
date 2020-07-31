using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Device
{
    public class DeviceInfoAndParameterBusiness : BaseBusiness<DeviceInfoAndParameter>, IDeviceInfoAndParameterBusiness, IDependency
    {
        #region 外部接口

        public List<DeviceInfoAndParameter> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<DeviceInfoAndParameter>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<DeviceInfoAndParameter, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public DeviceInfoAndParameter GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(DeviceInfoAndParameter data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(DeviceInfoAndParameter data)
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