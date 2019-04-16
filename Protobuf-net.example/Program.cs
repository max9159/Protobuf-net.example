using System;
using System.IO;

namespace Protobuf_net.example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Write protobuf content into file.
            var userInfo = new UserInfo("max")
            {
                ID = 1,
                Email = "max.chou.test@gmail.com",
                RegisterTime = DateTime.Now,
                Address = new Address { Line1 = "Taipei", Line2 = "Taiwan" }
            };

            using (var file = File.Create("userinfo.bin"))
            {
                ProtoBuf.Serializer.Serialize(file, userInfo);
            }

            UserInfo userInfoFromFile;
            using (var file = File.OpenRead("userinfo.bin"))
            {
                userInfoFromFile = ProtoBuf.Serializer.Deserialize<UserInfo>(file);
            }
            Console.WriteLine($"Read Protobuf content from file: Username:{ userInfoFromFile.Username }");

            //2. Try to use [ProtoAfterSerialization] with Validate()
            var emptyUserName = new UserInfo("");
            using (var file = File.Create("emptyUserName.bin"))
            {
                try
                {
                    ProtoBuf.Serializer.Serialize(file, emptyUserName); //Expected this line will occur an error.
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Try the validation mechanism and expected occur an error: { e.Message }");
                }
            }

            //3. object to text & text to object
            var userInfoForSerializer = new UserInfo("test_serializer")
            {
                ID = 2,
                Email = "test.serializer@gmail.com",
                RegisterTime = DateTime.Now,
                Address = new Address { Line1 = "Taipei", Line2 = "Taiwan" }
            };
            var protobufVal = ProtobufSerializer.SerializeToText(userInfoForSerializer);
            var userInfoObjFromText = ProtobufSerializer.DeSerializeFromText<UserInfo>(protobufVal);

            Console.WriteLine($"Read Protobuf content from plain text: Username:{ userInfoObjFromText.Username }");


            //4. Use Protobuf with Redis
            var redisHelper = new RedisHelper("localhost", 2);
            var inputKey = "protobuf_context_" + DateTime.Now.ToString("G");

            var userInfoForRedis = new UserInfo("test_Redis")
            {
                ID = 987,
                Email = "test.redis@gmail.com",
                RegisterTime = DateTime.Now,
                Address = new Address { Line1 = "Taipei", Line2 = "Taiwan" },
                Remark = "Remark"
            };
            redisHelper.Set(inputKey, userInfoForRedis);
            var userInfoFromRedis = redisHelper.Get<UserInfo>(inputKey);

            Console.WriteLine($"Read Protobuf content from Redis: Username:{ userInfoFromRedis.Username }");
            Console.Read();
        }
    }
}
