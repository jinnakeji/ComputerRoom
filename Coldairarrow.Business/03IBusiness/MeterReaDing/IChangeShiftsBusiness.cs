using Coldairarrow.Entity.MeterReaDing;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.MeterReaDing
{
    public interface IChangeShiftsBusiness
    {
        //List<ChangeShifts> GetDataList(Pagination pagination, string condition, string keyword);
        List<ChangeShiftsDTO> GetDataList(Pagination pagination, string condition, string keyword);
        ChangeShifts GetTheData(string id);
        AjaxResult AddData(ChangeShifts data);
        AjaxResult UpdateData(ChangeShifts data);
        AjaxResult DeleteData(List<string> ids);
    }
}