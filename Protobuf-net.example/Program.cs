using System;
using System.IO;

namespace Protobuf_net.example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userInfo = new UserInfo
            {
                ID = 1,
                Email = "max.chou.test@gmail.com",
                RegisterTime = DateTime.Now,
                Username = "max",
                Address = new Address { Line1 = "Taipei", Line2 = "Taiwan"}
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
            Console.WriteLine(userInfoFromFile.Username);
            Console.Read();
        }
    }
}
