using Coldairarrow.Business.DeviceThreshold;
using Coldairarrow.Entity.DeviceThreshold;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.DeviceThreshold
{
    [Route("/DeviceThreshold/[controller]/[action]")]
    public class Device_Switching_StateController : BaseApiController
    {
        #region DI

        public Device_Switching_StateController(IDevice_Switching_StateBusiness device_Switching_StateBus)
        {
            _device_Switching_StateBus = device_Switching_StateBus;
        }

        IDevice_Switching_StateBusiness _device_Switching_StateBus { get; }

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
        public ActionResult<AjaxResult<List<Device_Switching_State>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _device_Switching_StateBus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<Device_Switching_State>> GetTheData(string id)
        {
            var theData = _device_Switching_StateBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(Device_Switching_State data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _device_Switching_StateBus.AddData(data);
            }
            else
            {
                res = _device_Switching_StateBus.UpdateData(data);
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
            var res = _device_Switching_StateBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}