using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MeterReaDing
{
    /// <summary>
    /// 交接班日志表
    /// </summary>
    [Table("ChangeShifts")]
    public class ChangeShifts
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreatorRealName { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }

        /// <summary>
        /// 交班人员ID
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 接班人员ID
        /// </summary>
        public String ChangeUserId { get; set; }

        /// <summary>
        /// 报告地址
        /// </summary>
        public String filePath { get; set; }

        /// <summary>
        /// 报告备注信息
        /// </summary>
        public string remarks { get; set; }

    }
}