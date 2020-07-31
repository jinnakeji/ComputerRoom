using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IT_DevicePropBusiness
    {
        List<T_DeviceProp> GetDataList(Pagination pagination, string condition, string keyword);

        List<T_DeviceProp> GetList();

        List<V_DeviceProp> GetDataViewList(Pagination pagination, string condition, string keyword);

        List<T_DeviceProp> GetDataByDeviceId(int deviceTypeId);

        List<T_DeviceProp> GetDataList(int deviceTypeId);

        V_DeviceProp GetTheViewData(int id);

        T_DeviceProp GetTheData(string id);

        AjaxResult AddData(T_DeviceProp data);

        AjaxResult UpdateData(T_DeviceProp data);

        AjaxResult DeleteData(List<string> ids);
    }
}