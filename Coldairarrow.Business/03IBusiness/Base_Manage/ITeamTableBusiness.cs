using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface ITeamTableBusiness
    {
        List<TeamTable> GetDataList(Pagination pagination, string condition, string keyword);
        TeamTable GetTheData(string id);
        AjaxResult AddData(TeamTable data);
        AjaxResult UpdateData(TeamTable data);
        AjaxResult DeleteData(List<string> ids);
    }
}