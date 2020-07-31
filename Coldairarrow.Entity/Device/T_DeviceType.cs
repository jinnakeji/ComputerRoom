using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// T_DeviceType
    /// </summary>
    [Table("T_DeviceType")]
    public class T_DeviceType
    {

        /// <summary>
        /// id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 Id { get; set; }

        /// <summary>
        /// 设备类型名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

    }
}