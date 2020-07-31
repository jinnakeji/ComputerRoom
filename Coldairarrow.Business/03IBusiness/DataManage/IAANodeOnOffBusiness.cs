using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DataManage
{
    public interface IAANodeOnOffBusiness
    {
        //List<AANodeOnOff> GetDataList(Pagination pagination, string condition, string keyword);
        //AANodeOnOff GetTheData(string id);
        List<AANodeOnOffDTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null);
        AANodeOnOffDTO GetTheData(string id);
        AjaxResult AddData(AANodeOnOff data);
        AjaxResult UpdateData(AANodeOnOff data);
        AjaxResult DeleteData(List<string> ids);
    }
}