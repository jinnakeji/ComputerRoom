using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DeviceThreshold
{
    /// <summary>
    /// 湿度阈值设置
    /// </summary>
    [Table("Humidity_Threshold")]
    public class Humidity_Threshold
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
        /// 最高湿度
        /// </summary>
        public String Highest_Humidity { get; set; }

        /// <summary>
        /// 最低湿度
        /// </summary>
        public String Lowest_Humidity { get; set; }

        /// <summary>
        /// 阈值更新时间
        /// </summary>
        public DateTime? DeviceUpDateTime { get; set; }

    }
}