using Coldairarrow.Util;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IHomeBusiness
    {
        AjaxResult SubmitLogin(string userName, string password, bool isMd5 = false);
      
        AjaxResult ChangePwd(string oldPwd, string newPwd);

        AjaxResult CheckUser(string userName, string password);
    }
}
