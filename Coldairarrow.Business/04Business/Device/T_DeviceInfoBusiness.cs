using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Device
{
    public class T_DeviceInfoBusiness : BaseBusiness<T_DeviceInfo>, IT_DeviceInfoBusiness, IDependency
    {
        #region 外部接口

        public List<T_DeviceInfo> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<T_DeviceInfo>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<T_DeviceInfo, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public T_DeviceInfo GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(T_DeviceInfo data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(T_DeviceInfo data)
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