using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DataManage
{
    public interface IGroundResistanceBusiness
    {
        //List<GroundResistance> GetDataList(Pagination pagination, string condition, string keyword);
        //GroundResistance GetTheData(string id);
        List<GroundResistanceDTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null);
        GroundResistanceDTO GetTheData(string id);
        AjaxResult AddData(GroundResistance data);
        AjaxResult UpdateData(GroundResistance data);
        AjaxResult DeleteData(List<string> ids);
    }
}