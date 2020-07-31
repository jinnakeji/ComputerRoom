using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DataManage
{
    public interface INodeTempAndHumidityBusiness
    {
        //List<NodeTempAndHumidity> GetDataList(Pagination pagination, string condition, string keyword);
        //NodeTempAndHumidity GetTheData(string id);
        List<NodeTempAndHumidityDTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId=null, string keyword = null);
        NodeTempAndHumidityDTO GetTheData(string id);
        AjaxResult AddData(NodeTempAndHumidity data);
        AjaxResult UpdateData(NodeTempAndHumidity data);
        AjaxResult DeleteData(List<string> ids);
    }
}