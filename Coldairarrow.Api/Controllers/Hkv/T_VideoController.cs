using Coldairarrow.Business.Hkv;
using Coldairarrow.Entity.Hkv;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Hkv
{
    [Route("/Hkv/[controller]/[action]")]
    public class T_VideoController : BaseApiController
    {
        #region DI

        public T_VideoController(IT_VideoBusiness t_VideoBus)
        {
            _t_VideoBus = t_VideoBus;
        }

        IT_VideoBusiness _t_VideoBus { get; }

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
        public ActionResult<AjaxResult<List<T_Video>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _t_VideoBus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }
        [HttpGet]
        public ActionResult<AjaxResult<List<T_Video>>> GetDataAll()
        {
            var dataList = _t_VideoBus.GetDataAll();
            return Success(dataList);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<T_Video>> GetTheData(int id)
        {
            var theData = _t_VideoBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(T_Video data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _t_VideoBus.AddData(data);
            }
            else
            {
                res = _t_VideoBus.UpdateData(data);
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
            var res = _t_VideoBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}