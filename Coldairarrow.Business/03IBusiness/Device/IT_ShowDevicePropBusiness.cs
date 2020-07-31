using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IT_ShowDevicePropBusiness
    {
        List<V_ShowDeviceProp> GetDataList(Pagination pagination, string condition, string keyword);

        T_ShowDeviceProp GetTheData(string id);

        AjaxResult AddData(T_ShowDeviceProp data);

        AjaxResult UpdateData(T_ShowDeviceProp data);

        AjaxResult DeleteData(List<string> ids);

        AjaxResult SaveData(T_ShowDeviceProp model);
    }
}