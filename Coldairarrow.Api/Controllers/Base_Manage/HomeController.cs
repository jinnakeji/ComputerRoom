using Coldairarrow.Api.Models;
using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Business.Device;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    [Route("/Base_Manage/[controller]/[action]")]
    public class HomeController : BaseApiController
    {
        public HomeController(IHomeBusiness homeBus, IPermissionBusiness permissionBus, IBase_UserBusiness userBus, IDeviceDisplayModuleBusiness deviceInfoBusiness)
        {
            _homeBus = homeBus;
            _permissionBus = permissionBus;
            _userBus = userBus;
            _deviceDisplayModuleBus = deviceInfoBusiness;
        }
        IHomeBusiness _homeBus { get; }
        IPermissionBusiness _permissionBus { get; }
        IBase_UserBusiness _userBus { get; }
        IDeviceDisplayModuleBusiness _deviceDisplayModuleBus { get; }
        /// <summary>
        /// 首页设备数据
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(string
            condition, string keyword)
        {
            deviceDisplayModuleExt deviceDisplayModuleExt = new deviceDisplayModuleExt();
            var dataList = _deviceDisplayModuleBus.GetDataList(new Pagination(), condition, keyword);

            foreach (var v in dataList)
            {
                deviceDisplayModuleExt.deviceDisplayModule = v;
                deviceDisplayModuleExt.deviceInfos = null;

            }

            return Content(dataList.ToJson());
        }
        /// <summary>
        /// 用户登录(获取token)
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [CheckParamNotEmpty("userName", "password")]
        [NoCheckJWT]
        public ActionResult<AjaxResult> SubmitLogin(string userName, string password)
        {
            AjaxResult res = _homeBus.SubmitLogin(userName, password);

            return JsonContent(res.ToJson());
        }

        [HttpPost]
        [CheckParamNotEmpty]
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult<AjaxResult> CheckUser(string userName, string password)
        {
            return _homeBus.CheckUser(userName, password);
        }

        [HttpPost]
        [CheckParamNotEmpty("oldPwd", "newPwd")]
        public IActionResult ChangePwd(string oldPwd, string newPwd)
        {
            var res = _homeBus.ChangePwd(oldPwd, newPwd);

            return JsonContent(res.ToJson());
        }

        [HttpPost]
        public IActionResult GetOperatorInfo()
        {
            var theInfo = _userBus.GetTheData(Operator.UserId);
            var permissions = _permissionBus.GetUserPermissionValues(Operator.UserId);
            var resObj = new
            {
                UserInfo = theInfo,
                Permissions = permissions
            };

            return Success(resObj);
        }

        [HttpPost]
        public IActionResult GetOperatorMenuList()
        {
            var dataList = _permissionBus.GetUserMenuList(Operator.UserId);

            return Success(dataList);
        }
    }
}