using Coldairarrow.Business.Device;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Device
{
    [Route("/Device/[controller]/[action]")]
    public class T_DevicePropController : BaseApiController
    {
        #region DI

        public T_DevicePropController(IT_DevicePropBusiness t_DevicePropBus)
        {
            _t_DevicePropBus = t_DevicePropBus;
        }

        IT_DevicePropBusiness _t_DevicePropBus { get; }

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
        public ActionResult<AjaxResult<List<V_DeviceProp>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _t_DevicePropBus.GetDataViewList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }
        [HttpGet]
        public ActionResult<AjaxResult<List<T_DeviceProp>>> GetDataByDeviceTypeId(int deviceTypeId)
        {
            var dataList = _t_DevicePropBus.GetDataByDeviceId(deviceTypeId);

            return DataTable(dataList);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<V_DeviceProp>> GetTheData(int id)
        {
            var theData = _t_DevicePropBus.GetTheViewData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(T_DeviceProp data)
        {
            data.DisplayName = data.ProName;
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _t_DevicePropBus.AddData(data);
            }
            else
            {
                res = _t_DevicePropBus.UpdateData(data);
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
            var res = _t_DevicePropBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}