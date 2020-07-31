using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// 设备数据表
    /// </summary>
    [Table("DeviceData")]
    public class DeviceData
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreatorRealName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public String DeviceName { get; set; }

        /// <summary>
        /// 设备节点号
        /// </summary>
        public String DeviceNode { get; set; }

        /// <summary>
        /// 采集设备Mac地址
        /// </summary>
        public String DeviceMacCode { get; set; }

        /// <summary>
        /// 设备数据
        /// </summary>
        public Double? DeviceValue { get; set; }

        /// <summary>
        /// 设备参数ID
        /// </summary>
        public String DeviceParameter { get; set; }

    }
}