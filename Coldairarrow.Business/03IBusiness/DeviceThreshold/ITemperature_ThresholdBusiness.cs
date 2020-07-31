using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DeviceThreshold
{
    public interface ITemperature_ThresholdBusiness
    {
        List<Temperature_Threshold> GetDataList(Pagination pagination, string condition, string keyword);
        Temperature_Threshold GetTheData(string id);
        AjaxResult AddData(Temperature_Threshold data);
        AjaxResult UpdateData(Temperature_Threshold data);
        AjaxResult DeleteData(List<string> ids);
    }
}