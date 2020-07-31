using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// T_ShowDeviceProp
    /// </summary>
    [Table("T_ShowDeviceProp")]
    public class T_ShowDeviceProp
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 Id { get; set; }

        /// <summary>
        /// DeviceId
        /// </summary>
        public Int32 DeviceTypeId { get; set; } 

        /// <summary>
        /// PropId
        /// </summary>
        public Int32 PropId { get; set; }

        /// <summary>
        /// IsShow
        /// </summary>
        public Boolean IsShow { get; set; }

    }
}