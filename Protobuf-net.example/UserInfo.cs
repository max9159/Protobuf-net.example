using System;
using ProtoBuf;

namespace Protobuf_net.example
{
    [ProtoContract] // must declare the [ProtoContract], can not inherit it.
    public class UserInfo
    {
        [ProtoMember(1)] // must declare the [ProtoMember(number)]
        public int ID { get; set; }

        [ProtoMember(2)]
        public string Username { get; set; }

        [ProtoMember(3)]
        public string Email { get; set; }

        [ProtoIgnore] // Add this tag will be ignore during serialization
        public string Remark { get; set; }

        [ProtoMember(4)]
        public DateTime RegisterTtime { get; set; }
    }
}
