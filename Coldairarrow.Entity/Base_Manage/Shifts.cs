using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// 班次
    /// </summary>
    [Table("Shifts")]
    public class Shifts
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
        /// 班次名称
        /// </summary>
        public String ShiftsName { get; set; }

        /// <summary>
        /// 上班时间
        /// </summary>
        public String WorkTimeStart { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public String WorkTimeEnd { get; set; }

    }
}