using Coldairarrow.Business.Hkv;
using Coldairarrow.Entity.Hkv;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Hkv
{
    [Route("/Hkv/[controller]/[action]")]
    public class HkVideoController : BaseApiController
    {
        #region DI

        public HkVideoController(IHkVideoBusiness hkVideoBus)
        {
            _hkVideoBus = hkVideoBus;
        }

        IHkVideoBusiness _hkVideoBus { get; }

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
        public ActionResult<AjaxResult<List<HkVideo>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _hkVideoBus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<HkVideo>> GetTheData(string id)
        {
            var theData = _hkVideoBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(HkVideo data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _hkVideoBus.AddData(data);
            }
            else
            {
                res = _hkVideoBus.UpdateData(data);
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
            var res = _hkVideoBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}