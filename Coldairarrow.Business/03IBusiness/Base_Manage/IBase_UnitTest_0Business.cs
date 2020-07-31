using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_UnitTest_0Business
    {
        List<Base_UnitTest_0> GetDataList(Pagination pagination, string condition, string keyword);
        Base_UnitTest_0 GetTheData(string id);
        AjaxResult AddData(Base_UnitTest_0 data);
        AjaxResult UpdateData(Base_UnitTest_0 data);
        AjaxResult DeleteData(List<string> ids);
    }
}