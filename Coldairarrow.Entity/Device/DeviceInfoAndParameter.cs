using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// 设备信息与设备参数关联表
    /// </summary>
    [Table("DeviceInfoAndParameter")]
    public class DeviceInfoAndParameter
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 设备信息ID
        /// </summary>
        public String DeviceInfoId { get; set; }

        /// <summary>
        /// 设备参数ID
        /// </summary>
        public String DeviceParameterId { get; set; }

        /// <summary>
        /// 设备节点号
        /// </summary>
        public String DeviceNode { get; set; }

        /// <summary>
        /// 0显示1不显示
        /// </summary>
        public Boolean? IsDisplay { get; set; }

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

    }
}