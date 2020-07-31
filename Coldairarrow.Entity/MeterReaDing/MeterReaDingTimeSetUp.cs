using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MeterReaDing
{
    /// <summary>
    /// 抄表时间设置表
    /// </summary>
    [Table("MeterReaDingTimeSetUp")]
    public class MeterReaDingTimeSetUp
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
        /// 名称
        /// </summary>
        public string MeterName { get; set; }
        /// <summary>
        /// 抄表时间
        /// </summary>
        public string MeterTime { get; set; }

        public string RangeTime { get; set; }
        /// <summary>
        /// 抄表开始时间
        /// </summary>
        //public DateTime? StartTime { get; set; }

        /// <summary>
        /// 抄表结束时间
        /// </summary>
        //public DateTime? EndTime { get; set; }

    }
}