using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DataManage
{
    /// <summary>
    /// 电池电压
    /// </summary>
    [Table("ThreeElec")]
    public class ThreeElec
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
        /// 电压【A、B、C】
        /// </summary>
        [NotMapped]
        public String[] voltage { get; set; }
        public String voltageStr
        {
            get
            {
                if (voltage != null)
                {
                    return string.Join(",", this.voltage);
                }
                return null;
            }
            set
            {
                this.voltage = value != null ? value.Split(',') : null;
            }
        }
        /// <summary>
        /// 零电流
        /// </summary> 
        public String zeroCurrent { get; set; }

        /// <summary>
        /// 电流 ‘【A、B、C】
        /// </summary>
        [NotMapped]
        public String[] current { get; set; }
        public String currentStr
        {
            get
            {
                if (current != null)
                {
                    return string.Join(",", this.current);
                }
                return null;
            }
            set
            {
                this.current = value != null ? value.Split(',') : null;
            }
        }
        /// <summary>
        /// 总功率
        /// </summary>
        public String totalPower { get; set; }

        /// <summary>
        /// 设备数据采集时间
        /// </summary>
        public DateTime? updateTime { get; set; }

    }
}