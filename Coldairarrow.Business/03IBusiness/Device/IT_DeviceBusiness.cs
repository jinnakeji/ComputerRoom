using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IT_DeviceBusiness
    {
        List<T_Device> GetDataList(Pagination pagination, string condition, string keyword);

        List<T_Device> GetList();

        List<V_Device> GetDataViewList(Pagination pagination, string condition, string keyword);

        List<V_DeviceStructure> GetV_DeviceStructures();

        string GetDepartmentId(int deviceId);

        T_Device GetTheData(int id);

        V_Device GetTheDataByName(string name);

        AjaxResult AddData(List<T_Device> datas);

        AjaxResult AddData(T_Device data);

        AjaxResult UpdateData(T_Device data);

        AjaxResult DeleteData(List<string> ids);
    }
}