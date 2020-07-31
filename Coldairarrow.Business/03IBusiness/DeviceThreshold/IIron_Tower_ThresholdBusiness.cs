using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DeviceThreshold
{
    public interface IIron_Tower_ThresholdBusiness
    {
        List<Iron_Tower_Threshold> GetDataList(Pagination pagination, string condition, string keyword);
        Iron_Tower_Threshold GetTheData(string id);
        AjaxResult AddData(Iron_Tower_Threshold data);
        AjaxResult UpdateData(Iron_Tower_Threshold data);
        AjaxResult DeleteData(List<string> ids);
    }
}