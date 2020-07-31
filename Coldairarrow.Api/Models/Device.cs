using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Models
{
    public class Device : IEnumerable
    {
        public Device()
        {
            this.props = new Dictionary<string, string>();
        }

        Dictionary<string, string> props;

        public IEnumerable<string> Keys
        {
            get
            {
                var keys = props.Keys;
                foreach (var item in keys)
                {
                    yield return item;
                }
            }
        }

        public string this[string name]
        {
            get
            {
                return this.props[name];
            }
            set
            {
                this.props[name] = value;
            }
        }

        public string DeviceStatusEnum { get; set; }

        public int NodeNumber { get; set; }

        public Int64 TimeStamp { get; set; }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in props)
            {
                yield return item;
            }
        }
    }
}
