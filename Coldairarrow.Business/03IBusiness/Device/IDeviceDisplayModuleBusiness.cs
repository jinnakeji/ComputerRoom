using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IDeviceDisplayModuleBusiness
    {
        /// <summary>
        /// 根据条件获取模块
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="condition"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<DeviceDisplayModule> GetDataList(Pagination pagination, string condition, string keyword);

        List<DeviceDisplayModule> GetDataList();

        DeviceDisplayModule GetTheData(string id);
        AjaxResult AddData(DeviceDisplayModule data);
        AjaxResult UpdateData(DeviceDisplayModule data);
        AjaxResult DeleteData(List<string> ids);

        List<DeviceDisplayModule> GetDeviceModule(string departmentId);
        List<DeviceDisplayModule> GetDeviceModuleByDidByIsDisplay(string departmentId, int isDisplay);

        List<V_ModuleInfo> GetModuleByDepartmentId(string departmentId);
        List<V_ModuleInfo> GetVModeuleInfoList();
        
    }
}