using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using Coldairarrow.Entity.DataManage;
using Newtonsoft.Json.Linq;
using Coldairarrow.Business.DataManage;
using Coldairarrow.Entity.Device;
using Coldairarrow.Business.Device;
using System.Text;
using Microsoft.AspNetCore.Cors;

namespace Coldairarrow.Api.Controllers
{
    /// <summary>
    /// 远程调用
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class RemoteController : ControllerBase
    {
        public ILogger logger;
        public RemoteController(ILogger logger, IA5NodeOnOffBusiness a5NodeOnOffBus, IAANodeOnOffBusiness aANodeOnOffBus,
            IAngelBusiness angelBus, IBatteryBusiness batteryBus, ICO2Business cO2Bus, IGroundResistanceBusiness groundResistanceBus
            , INodeTempAndHumidityBusiness nodeTempAndHumidityBus, INodeTemperatureBusiness nodeTemperatureBus, IThreeElecBusiness threeElecBus, IDeviceInfoBusiness deviceInfoBus)
        {
            this.logger = logger;
            _a5NodeOnOffBus = a5NodeOnOffBus;
            _aANodeOnOffBus = aANodeOnOffBus;
            _angelBus = angelBus;
            _batteryBus = batteryBus;
            _cO2Bus = cO2Bus;
            _groundResistanceBus = groundResistanceBus;
            _nodeTempAndHumidityBus = nodeTempAndHumidityBus;
            _nodeTemperatureBus = nodeTemperatureBus;
            _threeElecBus = threeElecBus;
            _deviceInfoBus = deviceInfoBus;
        }

        IDeviceInfoBusiness _deviceInfoBus { get; }

        IThreeElecBusiness _threeElecBus { get; }
        INodeTemperatureBusiness _nodeTemperatureBus { get; }
        INodeTempAndHumidityBusiness _nodeTempAndHumidityBus { get; }
        IGroundResistanceBusiness _groundResistanceBus { get; }
        ICO2Business _cO2Bus { get; }
        IBatteryBusiness _batteryBus { get; }
        IAngelBusiness _angelBus { get; }
        IAANodeOnOffBusiness _aANodeOnOffBus { get; }
        IA5NodeOnOffBusiness _a5NodeOnOffBus { get; }
        /// <summary>
        /// post
        /// </summary>
        /// <returns></returns>
        [HttpPost] 
        public ActionResult<AjaxResult<object>> DeviceInfo()
        {
            StreamReader sr = new StreamReader(this.Request.Body, Encoding.UTF8);
            var text = System.Web.HttpUtility.UrlDecode(sr.ReadToEnd().Trim()).Replace("data", "").Replace("=", "");
            logger.Info(LogType.系统跟踪, "DeviceInfo:" + text); 
            try
            { 
                DeviceDataInfoStr devicelsit = JsonConvert.DeserializeObject<DeviceDataInfoStr>(text);   

                foreach (var item in devicelsit.deviceInfo)
                {
                    if (item != null)
                    {
                        item.deviceid = devicelsit.deviceid;
                        item.DeviceMacCode = devicelsit.mac != null ? devicelsit.mac : devicelsit.mac;
                        item.CreateTime = DateTime.Now;
                        _deviceInfoBus.AddData(item);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(LogType.系统异常, " 设备信息解析保存错误: " +ex.Message );
                throw;
            }


            return new JsonResult(new
            {
                success = true
            });
        }
        [HttpPost] 
        public ActionResult<AjaxResult<object>> DeviceData()
        {
            StreamReader sr = new StreamReader(this.Request.Body, Encoding.UTF8);
            var text = System.Web.HttpUtility.UrlDecode(sr.ReadToEnd().Trim()).Replace("data","").Replace("=","");
            logger.Info(LogType.系统异常, "DeviceData:" + text);

            try
            { 
                #region json 
                DeviceDataStr testModel = JsonConvert.DeserializeObject<DeviceDataStr>(text);

            foreach (var item in testModel.A5NodeOnOff)
            {
                if (item != null)
                {
                    item.mac = testModel.mac;
                    item.deviceid = testModel.deviceid;
                    item.updateTime = DateTime.Now;
                    _a5NodeOnOffBus.AddData(item);
                }
            }
            foreach (var item in testModel.AANodeOnOff)
            {
                if (item != null)
                {
                    item.mac = testModel.mac;
                    item.deviceid = testModel.deviceid;
                    item.updateTime = DateTime.Now;
                    _aANodeOnOffBus.AddData(item);
                }
            }
            foreach (var item in testModel.Angel)
            {
                if (item != null)
                {
                    item.mac = testModel.mac;
                    item.deviceid = testModel.deviceid;
                    item.updateTime = DateTime.Now;
                    _angelBus.AddData(item);
                }
            }
            foreach (var item in testModel.Battery)
            {
                if (item != null)
                {
                    item.mac = testModel.mac;
                    item.deviceid = testModel.deviceid;
                    item.updateTime = DateTime.Now;
                    _batteryBus.AddData(item);
                }
            }
            foreach (var item in testModel.CO2)
            {
                if (item != null)
                {
                    item.mac = testModel.mac;
                    item.deviceid = testModel.deviceid;
                    item.updateTime = DateTime.Now;
                    _cO2Bus.AddData(item);
                }
            }
            foreach (var item in testModel.GroundResistance)
            {
                if (item != null)
                {
                    item.mac = testModel.mac;
                    item.deviceid = testModel.deviceid;
                    item.updateTime = DateTime.Now;
                    _groundResistanceBus.AddData(item);
                }
            }
            foreach (var item in testModel.NodeTempAndHumidity)
            {
                if (item != null)
                {
                    item.mac = testModel.mac;
                    item.deviceid = testModel.deviceid;
                    item.updateTime = DateTime.Now;
                    _nodeTempAndHumidityBus.AddData(item);
                }
            }
            foreach (var item in testModel.NodeTemperature)
            {
                if (item != null)
                {
                    item.mac = testModel.mac;
                    item.deviceid = testModel.deviceid;
                    item.updateTime = DateTime.Now;
                    _nodeTemperatureBus.AddData(item);
                }
            }
            foreach (var item in testModel.ThreeElec)
            {
                if (item != null)
                {
                    item.mac = testModel.mac;
                    item.deviceid = testModel.deviceid;
                    item.updateTime = DateTime.Now;
                    _threeElecBus.AddData(item);
                }
            }


            }
            catch (Exception ex)
            {
                logger.Info(LogType.系统异常, " 设备数据解析保存错误: " + ex.Message);

                throw;
            }
            //testModel.DeviceDataStr.AANodeOnOff.ForEach(item =>_aANodeOnOffBus.AddData(item));
            //testModel.DeviceDataStr.Angel.ForEach(item => _angelBus.AddData(item));
            //testModel.DeviceDataStr.Battery.ForEach(item => _batteryBus.AddData(item));
            //testModel.DeviceDataStr.CO2.ForEach(item => _cO2Bus.AddData(item));
            //testModel.DeviceDataStr.GroundResistance.ForEach(item => _groundResistanceBus.AddData(item));
            //testModel.DeviceDataStr.NodeTempAndHumidity.ForEach(item => _nodeTempAndHumidityBus.AddData(item));
            //testModel.DeviceDataStr.NodeTemperature.ForEach(item => _nodeTemperatureBus.AddData(item));
            //testModel.DeviceDataStr.ThreeElec.ForEach(item => _threeElecBus.AddData(item));

            #endregion

            return new JsonResult(new
            {
                success = true
            });
        }
    }
    #region 将json格式的字符串 反序列化 成对象


    public class DeviceDataStr
    { /// <summary>
      /// 
      /// </summary>
        public string deviceid { get; set; }
        public string mac { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Angel> Angel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Battery> Battery { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<NodeTemperature> NodeTemperature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CO2> CO2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<GroundResistance> GroundResistance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<NodeTempAndHumidity> NodeTempAndHumidity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<A5NodeOnOff> A5NodeOnOff { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AANodeOnOff> AANodeOnOff { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ThreeElec> ThreeElec { get; set; }
       
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public DeviceDataStr DeviceDataStr { get; set; }
    }

    public class DeviceDataInfoStr
    {
        public string mac { get; set; }
        public string deviceid { get; set; }
        public List<DeviceInfo> deviceInfo { get; set; }
    }

    #endregion
}