using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IV_DeviceStructureBusiness
    {
        List<V_DeviceStructure> GetDataList(Pagination pagination, string condition, string keyword);
        V_DeviceStructure GetTheData(string id);
        AjaxResult AddData(V_DeviceStructure data);
        AjaxResult UpdateData(V_DeviceStructure data);
        AjaxResult DeleteData(List<string> ids);
    }
}