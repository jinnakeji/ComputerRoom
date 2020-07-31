using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IV_DevicePropBusiness
    {
        List<V_DeviceProp> GetDataList(Pagination pagination, string condition, string keyword);
        V_DeviceProp GetTheData(string id);
        AjaxResult AddData(V_DeviceProp data);
        AjaxResult UpdateData(V_DeviceProp data);
        AjaxResult DeleteData(List<string> ids);
    }
}