using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.DataManage
{
    public interface INodeTemperatureBusiness
    {
        //List<NodeTemperature> GetDataList(Pagination pagination, string condition, string keyword);
        //NodeTemperature GetTheData(string id);
        List<NodeTemperatureDTO> GetDataList(Pagination pagination, bool all, string userId = null, string ParentId = null, string keyword = null);
        NodeTemperatureDTO GetTheData(string id);
        AjaxResult AddData(NodeTemperature data);
        AjaxResult UpdateData(NodeTemperature data);
        AjaxResult DeleteData(List<string> ids);
    }
}