using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DeviceThreshold
{
    /// <summary>
    /// 电阻阈值设置
    /// </summary>
    [Table("Resistance_Threshold")]
    public class Resistance_Threshold
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
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
        /// 最高电阻
        /// </summary>
        public String Highest_Resistance { get; set; }

        /// <summary>
        /// 最低电阻
        /// </summary>
        public String Lowest_Resistance { get; set; }

        /// <summary>
        /// 阈值设置时间
        /// </summary>
        public DateTime? DeviceUpDateTime { get; set; }

    }
}