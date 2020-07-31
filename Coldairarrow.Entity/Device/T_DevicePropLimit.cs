using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// T_DevicePropLimit
    /// </summary>
    [Table("T_DevicePropLimit")]
    public class T_DevicePropLimit
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public int Id { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// 设备属性Id
        /// </summary>
        public int DevicePropId { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string Max { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string Min { get; set; }
    }
}