using Coldairarrow.Entity.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Models
{
    /// <summary>
    /// 首页设备数据用实体
    /// </summary>
    public class deviceDisplayModuleExt
    {
        public DeviceDisplayModule deviceDisplayModule { get; set; }

        public List<DeviceInfoExt> deviceInfos { get; set; }
    }
    public class DeviceInfoExt: DeviceInfo
    {
        public List<T> ts { get; set; }
    }
    public class T
    {
        Dictionary<string,string> keyValuePairs { get; set; }
    }
}
