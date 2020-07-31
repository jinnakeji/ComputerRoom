using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Coldairarrow.Business.Device
{
    public class DeviceDisplayModuleBusiness : BaseBusiness<DeviceDisplayModule>, IDeviceDisplayModuleBusiness, IDependency
    {
        #region 外部接口

        public List<DeviceDisplayModule> GetDataList(Pagination pagination, string condition, string keyword)
        {

            var q = Service.GetIQueryable<DeviceDisplayModule>();
            var where = LinqHelper.True<DeviceDisplayModule>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<DeviceDisplayModule, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public List<DeviceDisplayModule> GetDataList()
        {
            return GetIQueryable().ToList();
        }

        public DeviceDisplayModule GetTheData(string id)
        {
            return GetEntity(id);
        }

        public List<DeviceDisplayModule> GetDeviceModule(string departmentId)
        {
            //var data = GetIQueryable().Where(x => x.DepartmentId == departmentId);
            var data = GetIQueryable();
            return data.ToList();
        }
        public List<DeviceDisplayModule> GetDeviceModuleByDidByIsDisplay(string departmentId, int isdisplay)
        {
            //var data = GetIQueryable().Where(x => x.DepartmentId == departmentId & x.IsDisplay==isdisplay);
            var data = GetIQueryable().Where(x => x.IsDisplay == isdisplay);
            return data.ToList();
        }

        public AjaxResult AddData(DeviceDisplayModule data) 
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(DeviceDisplayModule data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public List<V_ModuleInfo> GetModuleByDepartmentId(string departmentId)
        {
            var q = Service.GetIQueryable<V_ModuleInfo>().Where(x => x.Departmentid == departmentId);
            return q.ToList();
        }
        public List<V_ModuleInfo> GetVModeuleInfoList()
        {
            var q = Service.GetIQueryable<V_ModuleInfo>();
            return q.ToList();
        }
        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
    public class DeviceDisplayModuleDTO : DeviceDisplayModule
    {
        public string DepartmentName { get; set; }
    }
}