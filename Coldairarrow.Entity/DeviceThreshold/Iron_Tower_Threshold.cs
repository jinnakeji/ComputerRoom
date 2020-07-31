using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DeviceThreshold
{
    /// <summary>
    /// 铁塔倾斜度阈值设置
    /// </summary>
    [Table("Iron_Tower_Threshold")]
    public class Iron_Tower_Threshold
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
        /// 铁塔X轴最高角度
        /// </summary>
        public String X_Highest { get; set; }

        /// <summary>
        /// 铁塔X轴最低角度
        /// </summary>
        public String X_Lowest { get; set; }

        /// <summary>
        /// 铁塔Y轴最高角度
        /// </summary>
        public String Y_Highest { get; set; }

        /// <summary>
        /// 铁塔Y轴最低角度
        /// </summary>
        public String Y_Lowest { get; set; }

        /// <summary>
        /// 阈值设置时间
        /// </summary>
        public DateTime? DeviceUpDateTime { get; set; }

    }
}