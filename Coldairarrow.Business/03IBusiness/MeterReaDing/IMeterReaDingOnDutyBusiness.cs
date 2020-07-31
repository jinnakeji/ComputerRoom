using Coldairarrow.Entity.MeterReaDing;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.MeterReaDing
{
    public interface IMeterReaDingOnDutyBusiness
    {
        List<MeterReaDingOnDuty> GetDataList(Pagination pagination, string condition, string keyword);
        List<MeterReaDingOnDuty> GetDataReport(string userid);
        List<MeterReaDingOnDutyDTO> GetDataDtoList(string deptId);
        MeterReaDingOnDuty GetTheData(string id);
        AjaxResult AddData(MeterReaDingOnDuty data);
        AjaxResult UpdateData(MeterReaDingOnDuty data);
        AjaxResult DeleteData(List<string> ids);
        List<MeterReaDingOnDuty> GetMeterReaDingBydeptIdBystartTimeByendTime(string deptId , string startTime, string endTime);
    }
}