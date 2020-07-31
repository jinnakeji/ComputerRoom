using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface ISchedulingBusiness
    {
        List<SchedulingDTO> GetDataList(Pagination pagination, string condition, string keyword);
        Scheduling GetTheData(string id);
        AjaxResult AddData(Scheduling data, string[] OnOffDate, string[] TeamTableId, string[] ShiftsId,string[] strRestDay);
        AjaxResult UpdateData(Scheduling data);
        AjaxResult DeleteData(List<string> ids);
        AjaxResult DeleteData();
    }
}