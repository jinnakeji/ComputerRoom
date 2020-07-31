using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Device
{
    public class T_DeviceTypeBusiness : BaseBusiness<T_DeviceType>, IT_DeviceTypeBusiness, IDependency
    {
        #region 外部接口

        public List<T_DeviceType> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<T_DeviceType>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<T_DeviceType, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public List<T_DeviceType> GetDataList()
        {
            return GetIQueryable().ToList();
        }

        public T_DeviceType GetTheData(int id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(T_DeviceType data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(T_DeviceType data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public T_DeviceType GetByTypeName(string deviceType)
        {
            return GetIQueryable().FirstOrDefault(x => x.Name == deviceType);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}