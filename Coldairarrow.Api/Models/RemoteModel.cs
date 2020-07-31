using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Models
{
    public class RemoteModel
    {
        public RemoteModel()
        {
            this.DeviceList = new List<Device>();
        }

        public string DeviceType { get; set; }

        public List<Device> DeviceList { get; set; }
    }
}
