﻿using System;
using ProtoBuf;

namespace Protobuf_net.example
{
    // must declare the [ProtoContract], can not inherit it.
    [ProtoContract(SkipConstructor = true)] 
    [ProtoInclude(6, typeof(Address))] // the number have to above than "UserInfo" maximum number.
    public class UserInfo
    {
        public UserInfo(string userName)
        {
            this.Username = userName;
        }

        [ProtoAfterSerialization]
        void Validate()
        {
            if (string.IsNullOrEmpty(this.Username))
                throw new Exception("The 'Username' is empty.");
        }

        [ProtoMember(1)] // must declare the [ProtoMember(number)]
        public int ID { get; set; }

        [ProtoMember(2)]
        public string Username { get; set; }

        [ProtoMember(3)]
        public string Email { get; set; }

        [ProtoIgnore] // Add this tag will be ignore during serialization
        public string Remark { get; set; }

        [ProtoMember(4)]
        public DateTime RegisterTime { get; set; }

        [ProtoMember(5)]
        public Address Address { get; set; }
    }

    [ProtoContract]
    public class Address
    {
        [ProtoMember(1)] // The child contract can start with 1.
        public string Line1 { get; set; }

        [ProtoMember(2)]
        public string Line2 { get; set; }

    }
}
