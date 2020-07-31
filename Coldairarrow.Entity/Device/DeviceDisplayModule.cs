using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// 首页数据显示模块
    /// </summary>
    [Table("DeviceDisplayModule")]
    public class DeviceDisplayModule
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
        /// 如： 动力监测，环境监测
        /// </summary>
        public String DeviceDisplayModuleName { get; set; }

        /// <summary>
        /// 1显示0不显示
        /// </summary>
        public int IsDisplay { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 所属部门Id
        /// </summary>
        //public String DepartmentId { get; set; }

    }
}