using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.Device
{
    public class V_DevicePropLimit
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public int Number { get; set; }

        public int PropId { get; set; }

        public string DeviceName { get; set; }

        public string DeviceTypeName { get; set; }

        public string PropName { get; set; }

        public string TypeName { get; set; }

        public string Max { get; set; }

        public string Min { get; set; }

        public int? LimitId { get; set; }
        public string DepartmentName { get; set; }
    }
}
