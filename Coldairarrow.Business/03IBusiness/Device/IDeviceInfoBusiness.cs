using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IDeviceInfoBusiness
    { 
        List<DeviceInfoDTO> GetDataList(Pagination pagination, bool all, string userId = null, string keyword = null);
        DeviceInfoDTO GetTheData(string id);
        List<DeviceInfo> GetControlDeviceInfo();
        AjaxResult AddData(DeviceInfo data);
        AjaxResult UpdateData(DeviceInfo data);
        AjaxResult DeleteData(List<string> ids);
        List<string> GetDeviceInfoDataList(string id);
    }
}