using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.Device
{
    public class V_ModuleInfo
    {
        public int Id { get; set; }

        public string Departmentid { get; set; }

        public string DepartmentName { get; set; }

        public int? Isdisplay { get; set; }

        public string Name { get; set; }

        public int Deviceid { get; set; }

        public string DeviceDisplayModuleId { get; set; }

        public string DeviceDisplayModuleName { get; set; }

        public int Sort { get; set; }

        public string DeviceName { get; set; }

        public bool DeviceStatus { get; set; }

        public int? DevicePropId { get; set; }

        public string DevicePropName { get; set; }

        public string DevicePropDisplayName { get; set; }

        public int? DeviceTypeId { get; set; }

        public string DeviceTypeName { get; set; }

        public string Max { get; set; }

        public string Min { get; set; }

        public string PropType { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string DevicePropUnit { get; set; }

        public bool DevicePropIsShow { get; set; }

        public int? Position { get; set; }
    }
}
