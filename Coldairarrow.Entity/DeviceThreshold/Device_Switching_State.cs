using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DeviceThreshold
{
    /// <summary>
    /// 设备开关状态
    /// </summary>
    [Table("Device_Switching_State")]
    public class Device_Switching_State
    {

        /// <summary>
        /// 主键
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// 设备节点号
        /// </summary>
        public String DeviceNode { get; set; }

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
        /// 设备开关状态 1 开启 0关闭  off/on
        /// </summary>
        public Int32? Switching_State { get; set; }

        /// <summary>
        /// 设备设置时间
        /// </summary>
        public DateTime? DeviceUpDateTime { get; set; }

    }
}