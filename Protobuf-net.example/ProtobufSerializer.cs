using System;
using System.IO;

namespace Protobuf_net.example
{
    public class ProtobufSerializer
    {
        public static string SerializeToText<T>(T obj)
        {
            using (var ms = new MemoryStream(1024))
            {
                ProtoBuf.Serializer.Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static T DeSerializeFromText<T>(string text)
        {
            var buffer = Convert.FromBase64String(text);
            using (var ms = new MemoryStream(buffer))
            {
                return ProtoBuf.Serializer.Deserialize<T>(ms);
            }
        }
    }
}