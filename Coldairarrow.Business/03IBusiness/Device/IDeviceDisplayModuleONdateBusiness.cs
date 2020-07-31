using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IDeviceDisplayModuleONdateBusiness
    {
        //List<DeviceDisplayModuleONdate> GetDataList(Pagination pagination, string condition, string keyword);
        List<V_DeviceDisplayModuleONdate> GetDataList(Pagination pagination, string condition, string keyword);

        List<DeviceDisplayModuleONdate> GetList();

        V_DeviceDisplayModuleONdate GetTheData(string id);

        AjaxResult AddData(DeviceDisplayModuleONdate data);

        AjaxResult UpdateData(DeviceDisplayModuleONdate data);

        AjaxResult DeleteData(List<string> ids);
        AjaxResult<List<V_DeviceDisplayModuleONdate>> GetDeviceDisplayModuleONdate(string departmentId);
    }
}