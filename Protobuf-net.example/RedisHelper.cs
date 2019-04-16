using System;
using System.Threading;
using StackExchange.Redis;

namespace Protobuf_net.example
{
    public class RedisHelper
    {
        private static Lazy<ConnectionMultiplexer> _conn;
        private const int RedisExpireMin = 1;

        /// <summary>
        /// </summary>
        /// <param name="connection">e.g. one instance: "127.0.0.1:6666", multiple :"127.0.0.1:7777,127.0.0.1:8888"</param>
        public RedisHelper(string connection)
        {
            _conn = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connection), LazyThreadSafetyMode.PublicationOnly);
        }

        public IDatabase GetDataBase(int databaseIndex)
        {
            return _conn.Value.GetDatabase(databaseIndex);
        }

        public bool IsExist(string key, int databaseIndex)
        {
            return _conn.Value.GetDatabase(databaseIndex).KeyExists(key);
        }

        public bool Remove(string key, int databaseIndex)
        {
            return _conn.Value.GetDatabase(databaseIndex).KeyDelete(key);
        }

        public string Get(string key, int databaseIndex)
        {
            return _conn.Value.GetDatabase(databaseIndex).StringGet(key);
        }

        public bool Set(string key, int databaseIndex, string value)
        {
            return _conn.Value.GetDatabase(databaseIndex).StringSet(key, value);
        }

    }
}
