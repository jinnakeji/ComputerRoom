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
    public class DeviceDisplayModuleONdateBusiness : BaseBusiness<DeviceDisplayModuleONdate>, IDeviceDisplayModuleONdateBusiness, IDependency
    {
        #region 外部接口

        public List<V_DeviceDisplayModuleONdate> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = Service.GetIQueryable<V_DeviceDisplayModuleONdate>();
            var where = LinqHelper.True<V_DeviceDisplayModuleONdate>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<V_DeviceDisplayModuleONdate, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }
            
            var result = from item in q.Where(@where) orderby item.DepartmentName, item.DeviceDisplayModuleName select item;
            pagination.Total = result.Count();
            return result.Skip((pagination.PageIndex - 1) * pagination.PageRows).Take(pagination.PageRows).ToList();
        }

        public V_DeviceDisplayModuleONdate GetTheData(string id)
        {
            return Service.GetEntity<V_DeviceDisplayModuleONdate>(id);
        }

        public AjaxResult AddData(DeviceDisplayModuleONdate data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(DeviceDisplayModuleONdate data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public AjaxResult<List<V_DeviceDisplayModuleONdate>> GetDeviceDisplayModuleONdate(string departmentId)
        {
            var data = Service.GetIQueryable<V_DeviceDisplayModuleONdate>().Where(x => x.DepartmentId == departmentId && x.IsDisplay == 1).ToList();
            return Success(data);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
    public class DeviceDisplayModuleONdateDTO : DeviceDisplayModuleONdate
    {
        public string DepartmentName { get; set; }
        public string Name { get; set; }

    }

}