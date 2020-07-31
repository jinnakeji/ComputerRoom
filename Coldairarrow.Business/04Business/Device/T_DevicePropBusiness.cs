using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Device
{
    public class T_DevicePropBusiness : BaseBusiness<T_DeviceProp>, IT_DevicePropBusiness, IDependency
    {
        #region 外部接口

        public List<T_DeviceProp> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<T_DeviceProp>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<T_DeviceProp, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public List<V_DeviceProp> GetDataViewList(Pagination pagination, string condition, string keyword)
        {
            var q = Service.GetIQueryable<V_DeviceProp>();
            var where = LinqHelper.True<V_DeviceProp>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<V_DeviceProp, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).OrderBy(x => x.DeviceTypeName).GetPagination(pagination).ToList();
        }

        public List<T_DeviceProp> GetDataByDeviceId(int deviceTypeId)
        {
            return GetIQueryable().Where(x => x.DeviceTypeId == deviceTypeId).ToList();
        }

        public List<T_DeviceProp> GetDataList(int deviceTypeId)
        {
            var q = GetIQueryable();
            return q.Where(x => x.DeviceTypeId == deviceTypeId).ToList();
        }

        public T_DeviceProp GetTheData(string id)
        {
            return GetEntity(id);
        }

        public V_DeviceProp GetTheViewData(int id)
        {
            return Service.GetEntity<V_DeviceProp>(id);
        }

        public AjaxResult AddData(T_DeviceProp data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(T_DeviceProp data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        #endregion 外部接口
    }
}