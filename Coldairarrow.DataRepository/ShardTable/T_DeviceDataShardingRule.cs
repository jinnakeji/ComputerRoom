using Coldairarrow.Entity.Device;
using EFCore.Sharding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.DataRepository.ShardTable
{
    public class T_DeviceDataShardingRule : AbsShardingRule<T_DeviceData>
    {
        public override DateTime BuildDate(T_DeviceData obj)
        {
            return obj.CreateTime;
        }
    }
}
