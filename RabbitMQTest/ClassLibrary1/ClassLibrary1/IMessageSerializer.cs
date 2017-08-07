using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public interface IMessageSerializer
    {
        byte[] SerializerByte<T>(T message) where T:new();
        string SerializerXmlString<T>(T message) where T :class, new();

        T Deserializer<T>(byte[] bytes) where T :class, new();
    }
}
