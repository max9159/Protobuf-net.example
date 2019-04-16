using System;
using System.Threading;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;

namespace Protobuf_net.example
{
    public class RedisHelper
    {
        private static Lazy<ConnectionMultiplexer> _conn;
        private const int RedisExpireMin = 1;
        private StackExchangeRedisCacheClient _client;
        private int _databaseIndex; 

        /// <summary>
        /// </summary>
        /// <param name="connection">e.g. one instance: "127.0.0.1:6666", multiple :"127.0.0.1:7777,127.0.0.1:8888"</param>
        public RedisHelper(string connection, int databaseIndex)
        {
            _databaseIndex = databaseIndex;
            _conn = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connection), LazyThreadSafetyMode.PublicationOnly);
            _client = new StackExchangeRedisCacheClient(new StackExchange.Redis.Extensions.Protobuf.ProtobufSerializer(), connection, _databaseIndex);
        }

        public IDatabase GetDataBase()
        {
            return _conn.Value.GetDatabase(_databaseIndex);
        }

        public bool IsExist(string key)
        {
            return _conn.Value.GetDatabase(_databaseIndex).KeyExists(key);
        }

        public bool Remove(string key)
        {
            return _conn.Value.GetDatabase(_databaseIndex).KeyDelete(key);
        }

        public string Get(string key)
        {
            return _conn.Value.GetDatabase(_databaseIndex).StringGet(key);
        }
        public T Get<T>(string key)
        {
            return _client.Get<T>(key);
        }

        public bool Set(string key, string value)
        {
            return _conn.Value.GetDatabase(_databaseIndex).StringSet(key, value);
        }

        public bool Set<T>(string key, T data)
        {
            return _client.Add<T>(key, data);
        }
    }
}
