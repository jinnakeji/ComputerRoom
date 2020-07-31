using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IDeviceInfoAndParameterBusiness
    {
        List<DeviceInfoAndParameter> GetDataList(Pagination pagination, string condition, string keyword);
        DeviceInfoAndParameter GetTheData(string id);
        AjaxResult AddData(DeviceInfoAndParameter data);
        AjaxResult UpdateData(DeviceInfoAndParameter data);
        AjaxResult DeleteData(List<string> ids);
    }
}