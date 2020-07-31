using Coldairarrow.Business.Device;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Device
{
    [Route("/Device/[controller]/[action]")]
    public class T_DevicePropLimitController : BaseApiController
    {
        #region DI

        public T_DevicePropLimitController(IT_DevicePropLimitBusiness t_DevicePropLimitBus,
            IT_DevicePropBusiness devicePropBusiness)
        {
            _t_DevicePropLimitBus = t_DevicePropLimitBus;
            this.devicePropBusiness = devicePropBusiness;
        }

        IT_DevicePropLimitBusiness _t_DevicePropLimitBus { get; }
        IT_DevicePropBusiness devicePropBusiness { get; }

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
        public ActionResult<AjaxResult<List<V_DevicePropLimit>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _t_DevicePropLimitBus.GetDataViewList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }
        [HttpPost]
        public ActionResult<AjaxResult<List<V_DeviceProp>>> GetDevicePropList(int deviceTypeId)
        {
            var data = this.devicePropBusiness.GetDataList(deviceTypeId);
            return Success(data);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="deviceId">设备Id</param>
        /// <param name="number">节点号</param>
        /// <param name="propId">属性Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<V_DevicePropLimit>> GetTheData(int deviceId, int number, int propId)
        {
            var theData = _t_DevicePropLimitBus.GetTheData(propId);
            if (theData == null)
            {
                theData = new V_DevicePropLimit();
            }
            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(V_DevicePropLimit data)
        {
            AjaxResult res;
            var theData = _t_DevicePropLimitBus.GetTheData(data.PropId);
            if (theData.LimitId == null)
            {
                res = _t_DevicePropLimitBus.AddData(new T_DevicePropLimit()
                {
                    DeviceId = data.DeviceId,
                    DevicePropId = data.PropId,
                    Max = data.Max,
                    Min = data.Min
                });
            }
            else
            {
                res = _t_DevicePropLimitBus.UpdateData(new T_DevicePropLimit()
                {
                    Id = theData.LimitId.Value,
                    DeviceId = data.DeviceId,
                    DevicePropId = data.PropId,
                    Max = data.Max,
                    Min = data.Min
                });
            }

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}