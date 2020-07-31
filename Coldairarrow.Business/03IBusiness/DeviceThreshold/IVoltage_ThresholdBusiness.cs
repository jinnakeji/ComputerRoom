using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DeviceThreshold
{
    public interface IVoltage_ThresholdBusiness
    {
        List<Voltage_Threshold> GetDataList(Pagination pagination, string condition, string keyword);
        Voltage_Threshold GetTheData(string id);
        AjaxResult AddData(Voltage_Threshold data);
        AjaxResult UpdateData(Voltage_Threshold data);
        AjaxResult DeleteData(List<string> ids);
    }
}