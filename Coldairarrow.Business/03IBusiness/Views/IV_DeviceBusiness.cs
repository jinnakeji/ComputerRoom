using Coldairarrow.Entity.views;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.views
{
    public interface IV_DeviceBusiness
    {
        List<V_Device> GetDataList(Pagination pagination, string condition, string keyword);
        V_Device GetTheData(string id);
        AjaxResult AddData(V_Device data);
        AjaxResult UpdateData(V_Device data);
        AjaxResult DeleteData(List<string> ids);
    }
}