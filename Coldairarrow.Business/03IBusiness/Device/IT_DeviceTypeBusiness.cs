using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IT_DeviceTypeBusiness
    {
        List<T_DeviceType> GetDataList(Pagination pagination, string condition, string keyword);

        List<T_DeviceType> GetDataList();

        T_DeviceType GetTheData(int id);
      
        AjaxResult AddData(T_DeviceType data);
      
        AjaxResult UpdateData(T_DeviceType data);
      
        AjaxResult DeleteData(List<string> ids);

        T_DeviceType GetByTypeName(string deviceType);
    }
}