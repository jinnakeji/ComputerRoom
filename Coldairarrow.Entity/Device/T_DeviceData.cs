using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// T_DeviceData
    /// </summary>
    [Table("T_DeviceData")]
    public class T_DeviceData
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 Id { get; set; }

        /// <summary>
        /// DeviceTypeId
        /// </summary>
        public Int32 DeviceTypeId { get; set; }

        /// <summary>
        /// DeviceId
        /// </summary>
        public Int32 DeviceId { get; set; }

        /// <summary>
        /// DevicePropId
        /// </summary>
        public Int32 DevicePropId { get; set; }
        public string DepartmentId { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public String Value { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}