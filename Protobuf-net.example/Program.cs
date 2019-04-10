using System;
using System.IO;

namespace Protobuf_net.example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userInfo = new UserInfo("max")
            {
                ID = 1,
                Email = "max.chou.test@gmail.com",
                RegisterTime = DateTime.Now,
                Address = new Address { Line1 = "Taipei", Line2 = "Taiwan"}
            };

            var emptyUserName = new UserInfo("");

            using (var file = File.Create("userinfo.bin"))
            {
                try
                {
                    ProtoBuf.Serializer.Serialize(file, userInfo);
                    ProtoBuf.Serializer.Serialize(file, emptyUserName); //Expected this line will occur an error.
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Expected occur an error: { e.Message }");
                }
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
