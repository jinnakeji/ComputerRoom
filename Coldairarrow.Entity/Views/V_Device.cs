using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.views
{
    /// <summary>
    /// V_Device
    /// </summary>
    [Table("V_Device")]
    public class V_Device
    {

        /// <summary>
        /// Id
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// DeviceTypeId
        /// </summary>
        public Int32 DeviceTypeId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Number
        /// </summary>
        public Int32 Number { get; set; }

        /// <summary>
        /// DeviceMacCode
        /// </summary>
        public String DeviceMacCode { get; set; }

        /// <summary>
        /// DeviceCode
        /// </summary>
        public String DeviceCode { get; set; }

        /// <summary>
        /// DeviceTypeName
        /// </summary>
        public String DeviceTypeName { get; set; }

    }
}