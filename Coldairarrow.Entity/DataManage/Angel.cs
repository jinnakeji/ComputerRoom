using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DataManage
{
    /// <summary>
    /// 角度
    /// </summary>
    [Table("Angel")]
    public class Angel
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
        /// 采集设备id
        /// </summary>
        public String deviceid { get; set; }

        /// <summary>
        ///  mac
        /// </summary>
        public String mac { get; set; }

        /// <summary>
        /// 原始温度
        /// </summary>
        public String temperatureOriginal { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public String temperature { get; set; }

        /// <summary>
        /// X角度
        /// </summary>
        public String xAngel { get; set; }

        /// <summary>
        /// X原始角度
        /// </summary>
        public String xAngelOriginal { get; set; }

        /// <summary>
        /// Y角度
        /// </summary>
        public String yAngel { get; set; }

        /// <summary>
        /// Y原始角度
        /// </summary>
        public String yAngelOriginal { get; set; }

        /// <summary>
        /// 设备数据采集时间
        /// </summary>
        public DateTime? updateTime { get; set; }

    }
}