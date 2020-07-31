using Coldairarrow.Entity.CallThe;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.CallThe
{
    public interface ICallThePoliceBusiness
    {
        //  List<CallThePolice> GetDataList(Pagination pagination, string condition, string keyword);
        List<CallThePoliceDTO> GetDataList(Pagination pagination, string condition, string keyword); 
          CallThePolice GetTheData(string id);
        AjaxResult AddData(CallThePolice data);
        AjaxResult UpdateData(CallThePolice data);
        AjaxResult DeleteData(List<string> ids);
    }
}