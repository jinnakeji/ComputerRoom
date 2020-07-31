using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IShiftsBusiness
    {
        List<Shifts> GetDataList(Pagination pagination, string condition, string keyword);
        Shifts GetTheData(string id);
        AjaxResult AddData(Shifts data);
        AjaxResult UpdateData(Shifts data);
        AjaxResult DeleteData(List<string> ids);
    }
}