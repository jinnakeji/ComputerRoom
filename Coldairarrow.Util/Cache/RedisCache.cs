using StackExchange.Redis;
using System;

namespace Coldairarrow.Util
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache : ICache
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">配置字符串</param>
        /// <param name="databaseIndex">数据库索引</param>
        public RedisCache(string config, int databaseIndex = 0)
        {
            _databaseIndex = databaseIndex;
            _redisConnection = ConnectionMultiplexer.Connect(config);
        }

        private ConnectionMultiplexer _redisConnection { get; }
        private IDatabase _db { get => _redisConnection.GetDatabase(_databaseIndex); }
        private int _databaseIndex { get; }
        public bool ContainsKey(string key)
        {
            return _db.KeyExists(key);
        }

        public object GetCache(string key)
        {
            var redisValue = _db.StringGet(key);
            if (!redisValue.HasValue)
                return null;
            ValueInfoEntry valueEntry = redisValue.ToString().ToObject<ValueInfoEntry>();
            object value;
            if (valueEntry.TypeName == typeof(string).AssemblyQualifiedName)
                value = valueEntry.Value;
            else
                value = valueEntry.Value.ToObject(Type.GetType(valueEntry.TypeName));

            if (valueEntry.ExpireTime != null && valueEntry.ExpireType == ExpireType.Relative)
                SetKeyExpire(key, valueEntry.ExpireTime.Value);

            return value;
        }

        public T GetCache<T>(string key) where T : class
        {
            return (T)GetCache(key);
        }

        public void SetKeyExpire(string key, TimeSpan expire)
        {
            _db.KeyExpire(key, expire);
        }

        public void RemoveCache(string key)
        {
            _db.KeyDelete(key);
        }

        public void SetCache(string key, object value)
        {
            _SetCache(key, value, null, null);
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            _SetCache(key, value, timeout, ExpireType.Absolute);
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            _SetCache(key, value, timeout, expireType);
        }

        private void _SetCache(string key, object value, TimeSpan? timeout, ExpireType? expireType)
        {
            string jsonStr = string.Empty;
            if (value is string)
                jsonStr = value as string;
            else
                jsonStr = value.ToJson();

            ValueInfoEntry entry = new ValueInfoEntry
            {
                Value = jsonStr,
                TypeName = value.GetType().AssemblyQualifiedName,
                ExpireTime = timeout,
                ExpireType = expireType
            };

            string theValue = entry.ToJson();
            if (timeout == null)
                _db.StringSet(key, theValue);
            else
                _db.StringSet(key, theValue, timeout);
        }

        public object GetCacheKeyList(string keys)
        {
            string keylist = null;
            //遍历集群内服务器
            foreach (var endPoint in _redisConnection.GetEndPoints())
            {
                //获取指定服务器
                var server = _redisConnection.GetServer(endPoint);
                //在指定服务器上使用 keys 或者 scan 命令来遍历key
                foreach (var key in server.Keys(0, keys))
                {
                    //获取key对于的值
                    var val = _db.StringGet(key);
                    keylist += key.ToString() + ",";
                   // Console.WriteLine($"key: {key}, value: {val}");
                }
            }
            return keylist;
        }
    }
}
