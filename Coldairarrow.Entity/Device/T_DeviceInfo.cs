using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// T_DeviceInfo
    /// </summary>
    [Table("T_DeviceInfo")]
    public class T_DeviceInfo
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
        /// 是否显示
        /// </summary>
        public Boolean IsShow { get; set; }

        /// <summary>
        /// 设备号码
        /// </summary>
        public Int32 Number { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}