using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DeviceThreshold
{
    public interface IDevice_Switching_StateBusiness
    {
        List<Device_Switching_State> GetDataList(Pagination pagination, string condition, string keyword);
        Device_Switching_State GetTheData(string id);
        AjaxResult AddData(Device_Switching_State data);
        AjaxResult UpdateData(Device_Switching_State data);
        AjaxResult DeleteData(List<string> ids);
    }
}