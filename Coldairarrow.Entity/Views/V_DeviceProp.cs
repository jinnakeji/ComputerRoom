using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Views
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
        public Int32 DeviceId { get; set; }

        /// <summary>
        /// Number
        /// </summary>
        public Int32 Number { get; set; }

        /// <summary>
        /// DeviceName
        /// </summary>
        public String DeviceName { get; set; }

        /// <summary>
        /// DeviceTypeName
        /// </summary>
        public String DeviceTypeName { get; set; }

        /// <summary>
        /// PropId
        /// </summary>
        public Int32 PropId { get; set; }

        /// <summary>
        /// PropName
        /// </summary>
        public String PropName { get; set; }

        /// <summary>
        /// TypeName
        /// </summary>
        public String TypeName { get; set; }

    }
}