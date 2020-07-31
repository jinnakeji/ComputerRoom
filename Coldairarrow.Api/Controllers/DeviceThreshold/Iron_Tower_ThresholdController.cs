using Coldairarrow.Business.DeviceThreshold;
using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.DeviceThreshold
{
    [Route("/DeviceThreshold/[controller]/[action]")]
    public class Iron_Tower_ThresholdController : BaseApiController
    {
        #region DI

        public Iron_Tower_ThresholdController(IIron_Tower_ThresholdBusiness iron_Tower_ThresholdBus)
        {
            _iron_Tower_ThresholdBus = iron_Tower_ThresholdBus;
        }

        IIron_Tower_ThresholdBusiness _iron_Tower_ThresholdBus { get; }

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
        public ActionResult<AjaxResult<List<Iron_Tower_Threshold>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _iron_Tower_ThresholdBus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<Iron_Tower_Threshold>> GetTheData(string id)
        {
            var theData = _iron_Tower_ThresholdBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(Iron_Tower_Threshold data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _iron_Tower_ThresholdBus.AddData(data);
            }
            else
            {
                res = _iron_Tower_ThresholdBus.UpdateData(data);
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
            var res = _iron_Tower_ThresholdBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}