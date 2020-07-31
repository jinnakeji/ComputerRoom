using Coldairarrow.Api.Hubs;
using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Business.Device;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Coldairarrow.Api.Controllers.Home
{
    [Route("/Home/[controller]/[action]")]
    public class IndexController : BaseApiController
    {
        #region DI

        public IndexController(IT_DeviceInfoBusiness t_DeviceInfoBus,
            IDeviceDisplayModuleBusiness deviceDisplayModuleBusiness,
            IBase_DepartmentBusiness departmentBusiness,
            IT_DeviceDataBusiness deviceDataBusiness, IDeviceDisplayModuleONdateBusiness deviceDisplayModuleONdateBus,
            IHubContext<RemoteHub> hubContext
            )
        {
            _t_DeviceInfoBus = t_DeviceInfoBus;
            this.deviceDisplayModuleBusiness = deviceDisplayModuleBusiness;
            this.departmentBusiness = departmentBusiness;
            this.deviceDataBusiness = deviceDataBusiness;
            _deviceDisplayModuleONdateBus = deviceDisplayModuleONdateBus;
            this.hubContext = hubContext;
        }

        private IT_DeviceInfoBusiness _t_DeviceInfoBus { get; }

        private IDeviceDisplayModuleBusiness deviceDisplayModuleBusiness { get; }

        private IDeviceDisplayModuleONdateBusiness _deviceDisplayModuleONdateBus { get; }
        private IBase_DepartmentBusiness departmentBusiness { get; }

        private IT_DeviceDataBusiness deviceDataBusiness { get; }

        private IHubContext<RemoteHub> hubContext { get; }

        #endregion DI

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<AjaxResult<V_ModuleInfo>> GetDataList(string departmentId)
        {
            var data = this.deviceDisplayModuleBusiness.GetModuleByDepartmentId(departmentId);
            return Success(data);
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<AjaxResult<Base_Department>> DepartmentList()
        {
            var data = departmentBusiness
                    .GetDepartMentList(Operator.Property.Id);
            //.Where(x => x.ParentId == null); 
            return Success(data);
        }

        [HttpGet]
        public ActionResult<AjaxResult<List<V_DeviceDisplayModuleONdate>>> ModulesList(string departmentId)
        {
            var result = _deviceDisplayModuleONdateBus.GetDeviceDisplayModuleONdate(departmentId);
            return result;
        }

        /// <summary>
        /// 获取最后一次的数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<AjaxResult<T_DeviceData>> GetDeviceDataByDepartmentId(string departmentId)
        {
            var datas = this.deviceDataBusiness.GetLastDataByDepartmentId(departmentId);
            return Success(datas);
        }

        /// <summary>
        /// 充电放电
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult> Electric(string type)
        {
            if (type == "+" || type == "-")
            {
                this.hubContext.Clients.All.SendAsync("Electric", type);
                return Success();
            }
            else
            {
                return Error("类型错误");
            }
        }
        [HttpPost]
        public ActionResult<AjaxResult> RemoveControl(int deviceId, string status)
        {
            string nodeNo = "";
            string deviceType = "";

            this.hubContext.Clients.All.SendAsync("Control", new { nodeNo, deviceType, status });
            return Success();
        }

        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult<AjaxResult> GetWeather(string cityId)
        {
            static string Unicode2String(string source)
            {
                return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                             source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
            }

            WebClient client = new WebClient();
            var str = client.DownloadString("https://tianqiapi.com/api?version=v6&appid=85866456&appsecret=tB8eMbDH&cityid=" + cityId);
            var content = Unicode2String(str);
            return Content(content);
        }
    }
}