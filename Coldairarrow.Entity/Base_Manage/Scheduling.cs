using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// 排班
    /// </summary>
    [Table("Scheduling")]
    public class Scheduling
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
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 班次Id
        /// </summary>
        public String ShiftsId { get; set; }

        /// <summary>
        /// 班组Id
        /// </summary>
        public String TeamTableId { get; set; }

        /// <summary>
        /// 上班日期
        /// </summary>
        public DateTime OfficeDate { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public DateTime GoOffWork { get; set; }
        //[NotMapped]
        //public string [] OnOffDate { get; set; }

    }
}