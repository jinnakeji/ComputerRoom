using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IDeviceParameterBusiness
    {
        List<DeviceParameter> GetDataList(Pagination pagination, string condition, string keyword);
        DeviceParameter GetTheData(string id);
        AjaxResult AddData(DeviceParameter data);
        AjaxResult UpdateData(DeviceParameter data);
        AjaxResult DeleteData(List<string> ids);
    }
}