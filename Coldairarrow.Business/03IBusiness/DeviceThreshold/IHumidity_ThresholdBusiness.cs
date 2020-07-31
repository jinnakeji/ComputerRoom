using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DeviceThreshold
{
    public interface IHumidity_ThresholdBusiness
    {
        List<Humidity_Threshold> GetDataList(Pagination pagination, string condition, string keyword);
        Humidity_Threshold GetTheData(string id);
        AjaxResult AddData(Humidity_Threshold data);
        AjaxResult UpdateData(Humidity_Threshold data);
        AjaxResult DeleteData(List<string> ids);
    }
}