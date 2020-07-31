using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DataManage
{
    public interface IA5NodeOnOffBusiness
    {
        //List<A5NodeOnOff> GetDataList(Pagination pagination, string condition, string keyword);
        //A5NodeOnOff GetTheData(string id);
        List<A5NodeOnOffDTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null);
        A5NodeOnOffDTO GetTheData(string id);
        AjaxResult AddData(A5NodeOnOff data);
        AjaxResult UpdateData(A5NodeOnOff data);
        AjaxResult DeleteData(List<string> ids);
    }
}