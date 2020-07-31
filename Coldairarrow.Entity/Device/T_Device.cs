using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// T_Device
    /// </summary>
    [Table("T_Device")]
    public class T_Device
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 Id { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public Int32 DeviceTypeId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 设备号码
        /// </summary>
        public Int32 Number { get; set; }
        public string DeviceMacCode { get; set; }
        public string DeviceCode { get; set; }
    }
}