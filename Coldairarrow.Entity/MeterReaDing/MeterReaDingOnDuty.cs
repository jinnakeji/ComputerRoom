using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MeterReaDing
{
    /// <summary>
    /// 值班抄表数据表
    /// </summary>
    [Table("MeterReaDingOnDuty")]
    public class MeterReaDingOnDuty
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
        /// 用户Id
        /// </summary>
        public String UserId { get; set; }
        /// <summary>
        /// 数据模块ID
        /// </summary>
        public String DeviceDisplayModuleID{get;set;}
        /// <summary>
        /// 数据内容
        /// </summary>
        public String DataContent { get; set; }
        /// <summary>
        /// 抄表时间Id
        /// </summary>
        public string MeterReaDingTimeSetUpId { get; set; }
        /// <summary>
        /// 指纹确认Id
        /// </summary>
        public string ConfirmUserId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string moduleName { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string deviceName { get; set; }
        /// <summary>
        /// 设备属性
        /// </summary>
        public string propName { get; set; }
        /// <summary>
        /// 设备属性数据
        /// </summary>
        public string propValue { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public string deptId { get; set; }


    }
}