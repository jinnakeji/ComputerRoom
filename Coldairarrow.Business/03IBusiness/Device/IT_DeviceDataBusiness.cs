using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IT_DeviceDataBusiness
    {
        List<T_DeviceData> GetDataList(Pagination pagination, string condition, string keyword);

        List<V_DeviceData> GetDataViewList(Pagination pagination, string condition, string keyword);

        T_DeviceData GetTheData(string id);

        List<T_DeviceData> GetLastDataByDepartmentId(string departmentId);

        List<V_LastData> GetLastData();

        AjaxResult AddData(List<T_DeviceData> datas);

        AjaxResult AddData(T_DeviceData data);

        AjaxResult UpdateData(T_DeviceData data);

        AjaxResult DeleteData(List<string> ids);

        void BulkInsert(List<T_DeviceData> datas);
    }
}