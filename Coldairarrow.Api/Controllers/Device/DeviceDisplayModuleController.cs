using Coldairarrow.Business.Device;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Device
{
    [Route("/Device/[controller]/[action]")]
    public class DeviceDisplayModuleController : BaseApiController
    {
        #region DI

        public DeviceDisplayModuleController(IDeviceDisplayModuleBusiness deviceDisplayModuleBus)
        {
            _deviceDisplayModuleBus = deviceDisplayModuleBus;
        }

        IDeviceDisplayModuleBusiness _deviceDisplayModuleBus { get; }

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
        public ActionResult<AjaxResult<List<DeviceDisplayModule>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _deviceDisplayModuleBus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        [HttpPost]
        public ActionResult<AjaxResult<DeviceDisplayModule>> GetDeviceModuleList(string departmentId)
        {
            var theData = _deviceDisplayModuleBus.GetDeviceModule(departmentId);

            return Success(theData);
        }
        [HttpPost]
        public ActionResult<AjaxResult<DeviceDisplayModule>> GetDeviceModuleByDidByIsDisplay(string departmentId, int isdisplay=1)
        {
            var theData = _deviceDisplayModuleBus.GetDeviceModuleByDidByIsDisplay(departmentId,isdisplay);

            return Success(theData);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<DeviceDisplayModule>> GetTheData(string id)
        {
            var theData = _deviceDisplayModuleBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(DeviceDisplayModule data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _deviceDisplayModuleBus.AddData(data);
            }
            else
            {
                res = _deviceDisplayModuleBus.UpdateData(data);
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
            var res = _deviceDisplayModuleBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}