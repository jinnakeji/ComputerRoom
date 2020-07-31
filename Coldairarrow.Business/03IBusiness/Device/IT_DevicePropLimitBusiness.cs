using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Device
{
    public interface IT_DevicePropLimitBusiness
    {
        List<T_DevicePropLimit> GetDataList(Pagination pagination, string condition, string keyword);

        List<V_DevicePropLimit> GetDataViewList(Pagination pagination, string condition, string keyword);

        V_DevicePropLimit GetTheData(int propId);

        AjaxResult AddData(T_DevicePropLimit data);

        AjaxResult UpdateData(T_DevicePropLimit data);

        AjaxResult DeleteData(List<string> ids);
    }
}