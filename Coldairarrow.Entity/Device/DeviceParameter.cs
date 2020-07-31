using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// 设备参数表
    /// </summary>
    [Table("DeviceParameter")]
    public class DeviceParameter
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
        /// 设备参数
        /// </summary>
        public String DeviceParameterName { get; set; }

    }
}