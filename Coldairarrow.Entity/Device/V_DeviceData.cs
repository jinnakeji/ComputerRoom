using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.Device
{
    public class V_DeviceData
    {
        public long Id { get; set; }
        public int DeviceTypeId { get; set; }
        public string DeviceTypeName { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int Number { get; set; }
        public int DevicePropId { get; set; }
        public string DevicePropName { get; set; }
        public string PropType { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }
        public string Value { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
