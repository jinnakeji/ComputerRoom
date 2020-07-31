using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DeviceThreshold
{
    public interface IThree_Electric_ThresholdBusiness
    {
        List<Three_Electric_Threshold> GetDataList(Pagination pagination, string condition, string keyword);
        Three_Electric_Threshold GetTheData(string id);
        AjaxResult AddData(Three_Electric_Threshold data);
        AjaxResult UpdateData(Three_Electric_Threshold data);
        AjaxResult DeleteData(List<string> ids);
    }
}