using Coldairarrow.Business.Device;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Device
{
    [Route("/Device/[controller]/[action]")]
    public class T_ShowDevicePropController : BaseApiController
    {
        #region DI

        public T_ShowDevicePropController(IT_ShowDevicePropBusiness t_ShowDevicePropBus)
        {
            _t_ShowDevicePropBus = t_ShowDevicePropBus;
        }

        IT_ShowDevicePropBusiness _t_ShowDevicePropBus { get; }

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
        public ActionResult<AjaxResult<List<V_ShowDeviceProp>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _t_ShowDevicePropBus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<T_ShowDeviceProp>> GetTheData(string id)
        {
            var theData = _t_ShowDevicePropBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(T_ShowDeviceProp data)
        {
            var res = _t_ShowDevicePropBus.SaveData(data);
            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public ActionResult<AjaxResult> DeleteData(string ids)
        {
            var res = _t_ShowDevicePropBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}