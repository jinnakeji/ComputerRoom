using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.CallThe
{
    /// <summary>
    /// 报警日志表
    /// </summary>
    [Table("CallThePolice")]
    public class CallThePolice
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public String DepartmentId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public String DeviceInfoId { get; set; }

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
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public String CallThePoliceContent { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public String Max { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public String Min { get; set; }

        /// <summary>
        /// 监测数据
        /// </summary>
        public String Value { get; set; }

    }
}