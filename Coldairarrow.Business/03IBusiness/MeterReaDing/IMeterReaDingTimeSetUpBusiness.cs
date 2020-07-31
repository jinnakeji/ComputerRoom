using Coldairarrow.Entity.MeterReaDing;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.MeterReaDing
{
    public interface IMeterReaDingTimeSetUpBusiness
    {
        List<MeterReaDingTimeSetUp> GetDataList(Pagination pagination, string condition, string keyword);

        List<MeterReaDingTimeSetUp> GetList();

        MeterReaDingTimeSetUp GetTheData(string id);

        AjaxResult AddData(MeterReaDingTimeSetUp data);

        AjaxResult UpdateData(MeterReaDingTimeSetUp data);

        AjaxResult DeleteData(List<string> ids);

        MeterReaDingTimeSetUp GetListByUserId(string userid);
    }
}