using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// V_DeviceDisplayModuleONdate
    /// </summary>
    [Table("V_DeviceDisplayModuleONdate")]
    public class V_DeviceDisplayModuleONdate
    {

        /// <summary>
        /// Id
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// IsDisplay
        /// </summary>
        public Int32? IsDisplay { get; set; }

        /// <summary>
        /// DepartmentId
        /// </summary>
        public String DepartmentId { get; set; }

        /// <summary>
        /// DeviceDisplayModuleId
        /// </summary>
        public String DeviceDisplayModuleId { get; set; }

        /// <summary>
        /// DeviceDisplayModuleName
        /// </summary>
        public String DeviceDisplayModuleName { get; set; }

        /// <summary>
        /// deviceid
        /// </summary>
        public int deviceid { get; set; }

        public int Sort { get; set; }

        public string DeviceCode { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// DepartmentName
        /// </summary>
        public String DepartmentName { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public Int32? Position { get; set; }

        /// <summary>
        /// DeviceMacCode
        /// </summary>
        public String DeviceMacCode { get; set; }

    }
}