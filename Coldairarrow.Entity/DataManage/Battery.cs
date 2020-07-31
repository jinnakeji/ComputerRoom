using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DataManage
{
    /// <summary>
    /// 电池电压
    /// </summary>
    [Table("Battery")]
    public class Battery
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
        /// 电压
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
        /// 电阻 
        /// </summary>
        [NotMapped]
        public String[] resistance { get; set; }

        public string resistanceStr
        {
            get
            {
                if (resistance != null)
                {
                    return string.Join(",", this.resistance);
                }
                return null;
            }
            set
            {
                this.resistance = value != null ? value.Split(',') : null;
            }
        }

        /// <summary>
        /// 电流 
        /// </summary>
        public String current { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public String temperature { get; set; }

        /// <summary>
        /// 设备数据采集时间
        /// </summary>
        public DateTime? updateTime { get; set; }

    }
}