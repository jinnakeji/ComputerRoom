using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_DepartmentController : BaseApiController
    {
        #region DI

        public Base_DepartmentController(IBase_DepartmentBusiness departmentBus)
        {
            _departmentBus = departmentBus;
        }

        IBase_DepartmentBusiness _departmentBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public ActionResult<AjaxResult<Base_Department>> GetTheData(string id)
        {
            var theData = _departmentBus.GetTheData(id) ?? new Base_Department();

            return Success(theData);
        }

        [HttpPost]
        public ActionResult<AjaxResult<List<Base_DepartmentTreeDTO>>> GetTreeDataList(string parentId)
        {
            List <Base_DepartmentTreeDTO> dataList = _departmentBus.GetTreeDataList(parentId);
             
            //if (dataList.IsNullOrEmpty())
            //{ 

            // dataList = _departmentBus.GetTreeDataList(parentId);

            //    CacheHelper.RedisCache.SetCache("Base_DepartmentTreeDTO", dataList);
            //}

            return Success(dataList);
        }
        [HttpPost]
        public ActionResult<AjaxResult<Base_Department>> GetDepartMentList(string parentId) 
        {
            string uid = Operator.UserId; 

            List<Base_Department> departmetlist = new List<Base_Department>();
            departmetlist = _departmentBus.GetDepartMentList(uid);

            return  Success(departmetlist);
        }

        #endregion

        #region 提交

        [HttpPost]
        public ActionResult<AjaxResult> SaveData(Base_Department theData)
        {
            AjaxResult res;
            if (theData.Id.IsNullOrEmpty())
            {
                theData.InitEntity();

                res = _departmentBus.AddData(theData);
            }
            else
            {
                res = _departmentBus.UpdateData(theData);
            }

            return JsonContent(res.ToJson());
        }

        [HttpPost]
        public ActionResult<AjaxResult> DeleteData(string ids)
        {
            var res = _departmentBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}