using Coldairarrow.Business.Device;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Device
{
    [Route("/Device/[controller]/[action]")]
    public class T_DeviceController : BaseApiController
    {
        #region DI

        public T_DeviceController(IT_DeviceBusiness deviceBus, IT_DeviceTypeBusiness typeBusiness)
        {
            this._deviceBus = deviceBus;
            this._typeBusiness = typeBusiness;
        }

        IT_DeviceBusiness _deviceBus { get; }
        IT_DeviceTypeBusiness _typeBusiness { get; }

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
        public ActionResult<AjaxResult<List<V_Device>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _deviceBus.GetDataViewList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<T_Device>> GetTheData(int id)
        {
            var theData = _deviceBus.GetTheData(id);

            return Success(theData);
        }

        [HttpPost]
        public ActionResult<AjaxResult<T_DeviceType>> GetDeviceTypeList()
        {
            var result = _typeBusiness.GetDataList();
            return Success(result);
        }

        [HttpPost]
        public ActionResult<AjaxResult<V_Device>> GetDataViewList(Pagination pagination, string condition, string keyword)
        {
            var result = _deviceBus.GetDataViewList(pagination, condition, keyword);
            return Success(result);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(T_Device data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _deviceBus.AddData(data);
            }
            else
            {
                res = _deviceBus.UpdateData(data);
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
            var res = _deviceBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}