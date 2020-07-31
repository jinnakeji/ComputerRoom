using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DeviceThreshold
{
    public interface ICurrent_ThresholdBusiness
    {
        List<Current_Threshold> GetDataList(Pagination pagination, string condition, string keyword);
        Current_Threshold GetTheData(string id);
        AjaxResult AddData(Current_Threshold data);
        AjaxResult UpdateData(Current_Threshold data);
        AjaxResult DeleteData(List<string> ids);
    }
}