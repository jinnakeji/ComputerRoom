using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DataManage
{
    public interface IAngelBusiness
    {
        //List<Angel> GetDataList(Pagination pagination, string condition, string keyword);
        //Angel GetTheData(string id);
        List<AngelDTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null);
        AngelDTO GetTheData(string id);
        AjaxResult AddData(Angel data);
        AjaxResult UpdateData(Angel data);
        AjaxResult DeleteData(List<string> ids);
    }
}