using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_UnitTest_0Controller : BaseApiController
    {
        #region DI

        public Base_UnitTest_0Controller(IBase_UnitTest_0Business base_UnitTest_0Bus)
        {
            _base_UnitTest_0Bus = base_UnitTest_0Bus;
        }

        IBase_UnitTest_0Business _base_UnitTest_0Bus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="condition">查询字段</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<List<Base_UnitTest_0>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _base_UnitTest_0Bus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<Base_UnitTest_0>> GetTheData(string id)
        {
            var theData = _base_UnitTest_0Bus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(Base_UnitTest_0 data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _base_UnitTest_0Bus.AddData(data);
            }
            else
            {
                res = _base_UnitTest_0Bus.UpdateData(data);
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public ActionResult<AjaxResult> DeleteData(string ids)
        {
            var res = _base_UnitTest_0Bus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}