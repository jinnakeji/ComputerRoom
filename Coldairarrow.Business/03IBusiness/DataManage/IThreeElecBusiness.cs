using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DataManage
{
    public interface IThreeElecBusiness
    {
        //List<ThreeElec> GetDataList(Pagination pagination, string condition, string keyword);
        //ThreeElec GetTheData(string id);
        List<ThreeElecDTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null);
        ThreeElecDTO GetTheData(string id);
        AjaxResult AddData(ThreeElec data);
        AjaxResult UpdateData(ThreeElec data);
        AjaxResult DeleteData(List<string> ids);
    }
}