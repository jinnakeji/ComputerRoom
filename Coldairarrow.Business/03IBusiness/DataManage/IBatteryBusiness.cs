using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DataManage
{
    public interface IBatteryBusiness
    {
        //List<Battery> GetDataList(Pagination pagination, string condition, string keyword);
        //Battery GetTheData(string id);
        List<BatteryDTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null);
        BatteryDTO GetTheData(string id);
        AjaxResult AddData(Battery data);
        AjaxResult UpdateData(Battery data);
        AjaxResult DeleteData(List<string> ids);
    }
}