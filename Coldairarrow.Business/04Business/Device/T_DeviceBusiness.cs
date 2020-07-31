using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Device
{
    public class T_DeviceBusiness : BaseBusiness<T_Device>, IT_DeviceBusiness, IDependency
    {
        #region 外部接口

        public List<T_Device> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<T_Device>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<T_Device, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public List<V_Device> GetDataViewList(Pagination pagination, string condition, string keyword)
        {
            var q = this.Service.GetIQueryable<V_Device>();
            var where = LinqHelper.True<V_Device>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<V_Device, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public T_Device GetTheData(int id)
        {
            return GetEntity(id);
        }
        public string GetDepartmentId(int deviceId)
        {
            string departid= Service.GetIQueryable<V_ModuleInfo>().FirstOrDefault(t => t.Deviceid == deviceId)?.Departmentid;
          
            return departid;
        }
        public List<V_DeviceStructure> GetV_DeviceStructures()
        {
            return Service.GetIQueryable<V_DeviceStructure>().ToList();
        }

        public V_Device GetTheDataByName(string name)
        {
           return Service.GetIQueryable<V_Device>().FirstOrDefault(t => t.DeviceTypeName == name);
        }
        public AjaxResult AddData(T_Device data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(T_Device data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public AjaxResult AddData(List<T_Device> datas)
        {
            Insert(datas);

            return Success();
        }

      


        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}