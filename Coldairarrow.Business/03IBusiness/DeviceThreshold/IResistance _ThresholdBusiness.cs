using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DeviceThreshold
{
    public interface IResistance_ThresholdBusiness
    {
        List<Resistance_Threshold> GetDataList(Pagination pagination, string condition, string keyword);
        Resistance_Threshold GetTheData(string id);
        AjaxResult AddData(Resistance_Threshold data);
        AjaxResult UpdateData(Resistance_Threshold data);
        AjaxResult DeleteData(List<string> ids);
    }
}