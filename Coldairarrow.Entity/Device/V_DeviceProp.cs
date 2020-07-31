using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// V_DeviceProp
    /// </summary>
    [Table("V_DeviceProp")]
    public class V_DeviceProp
    {

        /// <summary>
        /// Id
        /// </summary>
        public Int32? Id { get; set; }
        /// <summary>
        /// DeviceId
        /// </summary>
        public Int32 DeviceTypeId { get; set; }
        /// <summary>
        /// Number
        /// </summary>
        public Int32 TypeId { get; set; }
        /// <summary>
        /// DeviceName
        /// </summary>
        public String DeviceTypeName { get; set; }
        /// <summary>
        /// DeviceTypeName
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// PropName
        /// </summary>
        public String TypeName { get; set; }

        public string Unit { get; set; }

        /// <summary>
        /// TypeName
        /// </summary>
        public String DisplayName { get; set; }
        /// <summary>
        /// 属性中文名
        /// </summary>
        public string ProName { get; set; }

    }
}