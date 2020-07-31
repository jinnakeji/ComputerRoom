using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// T_DeviceProp
    /// </summary>
    [Table("T_DeviceProp")]
    public class T_DeviceProp
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 Id { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Int32 TypeId { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        public Int32 DeviceTypeId { get; set; }

        /// <summary>
        /// 单位 
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 属性中文名
        /// </summary>
        public string DisplayName { get;set;}
        [NotMapped]
        public string ProName { get; set; }
    }
}