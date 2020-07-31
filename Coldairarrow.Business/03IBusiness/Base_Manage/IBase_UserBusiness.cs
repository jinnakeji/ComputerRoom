using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_UserBusiness
    {
        List<Base_UserDTO> GetDataList(Pagination pagination, bool all, string userId = null, string keyword = null, string departmentid = null, string teamtableid = null);
        List<SelectOption> GetOptionList(string selectedValueJson, string q);
        Base_UserDTO GetTheData(string id);
        List<Base_UserDTO> GetUserByTeamIdDataList(string temid, bool all);
        AjaxResult AddData(Base_User newData, List<string> roleIds);
        AjaxResult UpdateData(Base_User theData, List<string> roleIds);
        AjaxResult DeleteData(List<string> ids);
        AjaxResult UpdateFinger(string id, string template);
        Base_User  GetUserListByNameByPwd(string name,string pwd);
    }
}