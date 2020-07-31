using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IT_DeviceInfoBusiness
    {
        List<T_DeviceInfo> GetDataList(Pagination pagination, string condition, string keyword);
        T_DeviceInfo GetTheData(string id);
        AjaxResult AddData(T_DeviceInfo data);
        AjaxResult UpdateData(T_DeviceInfo data);
        AjaxResult DeleteData(List<string> ids);
    }
}