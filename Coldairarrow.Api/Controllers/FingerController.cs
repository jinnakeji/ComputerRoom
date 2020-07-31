using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using libzkfpcsharp;

namespace Coldairarrow.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FingerController : BaseApiController
    {
        static IntPtr dbHandler;
        IBase_UserBusiness userBusiness;
        IHomeBusiness homeBusiness;

        static FingerController()
        {
            zkfp2.Init();
            dbHandler = zkfp2.DBInit();
        }

        public FingerController(IBase_UserBusiness userBusiness, IHomeBusiness homeBusiness)
        {
            this.userBusiness = userBusiness;
            this.homeBusiness = homeBusiness;
        }

        /// <summary>
        /// 录入指纹信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <param name="temp1"></param>
        /// <param name="temp2"></param>
        /// <param name="temp3"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult> Input(string id, string temp1, string temp2, string temp3)
        {
            var arr1 = zkfp2.Base64ToBlob(temp1);
            var arr2 = zkfp2.Base64ToBlob(temp2);
            var arr3 = zkfp2.Base64ToBlob(temp3);

            var temp = new byte[2048];
            var len = temp.Length;
            var res = zkfp2.DBMerge(dbHandler, arr1, arr2, arr3, temp, ref len);
            if (res != 0)
            {
                return Error("");
            }

            var result = userBusiness.UpdateFinger(id, zkfp2.BlobToBase64(temp, len));

            if (result.Success)
            {
                return Success();
            }
            else
            {
                return Error(result.Msg);
            }
        }

        [NoCheckJWT]
        [HttpPost]
        public ActionResult<AjaxResult<string>> Identity(string template)
        {
            var userDtoList = userBusiness.GetDataList(new Pagination()
            {
                PageIndex = 1,
                PageRows = int.MaxValue,
                SortField = "Id",
                SortType = "ASC"
            }, true).Where(x => x.FingerTemplate != null);

            foreach (var userDto in userDtoList)
            {
                var ret = zkfp2.DBMatch(dbHandler, zkfp2.Base64ToBlob(userDto.FingerTemplate), zkfp2.Base64ToBlob(template));
                if (ret > 0)
                {
                    var res = homeBusiness.SubmitLogin(userDto.UserName, userDto.Password, true);

                    return JsonContent(res.ToJson());
                }
            }

            return Error("匹配失败！");
        }

        [HttpPost]
        [NoCheckJWT]
        public ActionResult<AjaxResult<Base_UserDTO>> GetUserByTemplate(string template)
        {
            var userDtoList = userBusiness.GetDataList(new Pagination()
            {
                PageIndex = 1,
                PageRows = int.MaxValue,
                SortField = "Id",
                SortType = "ASC"
            }, true).Where(x => x.FingerTemplate != null);

            foreach (var userDto in userDtoList)
            {
                var ret = zkfp2.DBMatch(dbHandler, zkfp2.Base64ToBlob(userDto.FingerTemplate), zkfp2.Base64ToBlob(template));
                if (ret > 0)
                {
                    return Success(new
                    {
                        userDto.UserName,
                        userDto.Id
                    });
                }
            }

            return Error("未找到用户！");
        }
    }
}