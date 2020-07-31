using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.Device
{
    public class V_ShowDeviceProp
    {
        public int Id { get; set; }

        public int DeviceTypeId { get; set; }
        
        public int PropId { get; set; }
        
        public string DeviceTypeName { get; set; }
        
        public string PropName { get; set; }
        
        public bool IsShow { get; set; }
    }
}
