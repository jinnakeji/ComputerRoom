using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.MeterReaDing
{
    /// <summary>
    /// 报表
    /// </summary>
    public class DailyReport
    {
         
        public string moduleId { get; set; }
        public string moduleName { get; set; }
        public int position { get; set; }

        public List<Device> deviceList { get; set; }
    }
    public class Device
    {
        public string deviceId { get; set; }
        public string deviceName { get; set; }
        public string deviceTypeName { get; set; }

        public List<Prop> propList { get; set; }

    }
    public class Prop
    {
        public string propId { get; set; }

        public string propName { get; set; }
        public string displayName { get; set; }

        public string max { get; set; }
        public string min { get; set; }

        public string value { get; set; }
    }
    public enum DeviceType
    {
        /// <summary>
        /// 温度
        /// </summary>
        temperature,
        /// <summary>
        /// 湿度
        /// </summary>
        humidity,
        /// <summary>
        /// 电阻
        /// </summary>
        resistance,
        /// <summary>
        /// 电压【A、B、C】
        /// </summary>
        voltage,
        /// <summary>
        /// 电流 【A、B、C】 
        /// </summary>
        current,
        /// <summary>
        /// 原始温度
        /// </summary>
        temperatureOriginal,
        /// <summary>
        /// X角度
        /// </summary>
        xAngel,
        /// <summary>
        /// X原始角度
        /// </summary>
        xAngelOriginal,
        /// <summary>
        /// Y角度
        /// </summary>
        yAngel,
        /// <summary>
        /// Y原始角度
        /// </summary>
        yAngelOriginal,
        /// <summary>
        /// 二氧化碳
        /// </summary>
        value,
        /// <summary>
        /// 零电流
        /// </summary>
        zeroCurrent,
        /// <summary>
        /// 总功率
        /// </summary>
        totalPower,
        /// <summary>
        /// 开启/关闭
        /// </summary>
        onOff
    }
    public class EnumTypeExample
    {
        public static string PrintDeviceType(DeviceType mytype)
        {
            string tmpType = string.Empty;
            switch (mytype)
            {
                case DeviceType.temperature:
                    tmpType = "温度";
                    break;
                case DeviceType.humidity:
                    tmpType = "湿度";
                    break;

                case DeviceType.resistance:
                    tmpType = "电阻";
                    break;

                case DeviceType.voltage:
                    tmpType = "电压【A、B、C】";
                    break;
                case DeviceType.current:
                    tmpType = "电流 【A、B、C】";
                    break;

                case DeviceType.temperatureOriginal:
                    tmpType = "原始温度";
                    break;

                case DeviceType.xAngel:
                    tmpType = "X角度";
                    break;

                case DeviceType.xAngelOriginal:
                    tmpType = "X原始角度";
                    break;

                case DeviceType.yAngel:
                    tmpType = "Y角度";
                    break;

                case DeviceType.yAngelOriginal:
                    tmpType = "Y原始角度";
                    break;
                case DeviceType.value:
                    tmpType = "二氧化碳";
                    break;

                case DeviceType.zeroCurrent:
                    tmpType = "零电流";
                    break;

                case DeviceType.totalPower:
                    tmpType = "总功率";
                    break;


                case DeviceType.onOff:
                    tmpType = "开关状态";
                    break;
            }
            return tmpType;
        }
    }
}