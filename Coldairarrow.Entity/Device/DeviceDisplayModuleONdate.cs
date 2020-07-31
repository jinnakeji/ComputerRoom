using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// 显示模块与设备与部门关联
    /// </summary>
    [Table("DeviceDisplayModuleONdate")]
    public class DeviceDisplayModuleONdate
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
        /// 模块ID
        /// </summary>
        public String DeviceDisplayModuleId { get; set; }

        /// <summary>
        /// 1显示0不显示
        /// </summary>
        public Int32? IsDisplay { get; set; }

        /// <summary>
        /// 所属部门Id
        /// </summary>
        public String DepartmentId { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public Int32? DeviceId { get; set; }

    }
}