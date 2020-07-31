using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DataManage
{
    public interface ICO2Business
    {
        //List<CO2> GetDataList(Pagination pagination, string condition, string keyword);
        //CO2 GetTheData(string id);
        List<CO2DTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null);
        CO2DTO GetTheData(string id);
        AjaxResult AddData(CO2 data);
        AjaxResult UpdateData(CO2 data);
        AjaxResult DeleteData(List<string> ids);
    }
}