using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DataManage
{
    /// <summary>
    /// 温湿度
    /// </summary>
    [Table("NodeTempAndHumidity")]
    public class NodeTempAndHumidity
    {

        /// <summary>
        /// 主键
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
        /// 设备节点号
        /// </summary>
        public String nodeNumber { get; set; }

        /// <summary>
        /// 正常温度
        /// </summary>
        public String temperature { get; set; }

        /// <summary>
        /// 正常湿度
        /// </summary>
        public String humidity { get; set; }

        /// <summary>
        /// 设备数据采集时间
        /// </summary>
        public DateTime? updateTime { get; set; }

        /// <summary>
        /// 采集设备id
        /// </summary>
        public String deviceid { get; set; }

        /// <summary>
        ///  mac
        /// </summary>
        public String mac { get; set; }

    }
}