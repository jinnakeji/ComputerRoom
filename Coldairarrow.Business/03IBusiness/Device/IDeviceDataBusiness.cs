using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IDeviceDataBusiness
    {
        List<DeviceData> GetDataList(Pagination pagination, string condition, string keyword);
        DeviceData GetTheData(string id);
        AjaxResult AddData(DeviceData data);
        AjaxResult UpdateData(DeviceData data);
        AjaxResult DeleteData(List<string> ids);
    }
}