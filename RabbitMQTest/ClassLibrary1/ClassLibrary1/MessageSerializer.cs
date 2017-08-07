using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
using System.IO;
namespace RabbitMQClient
{
    [Serializable]
    public class MessageSerializer:IMessageSerializer
    {
        public byte[] SerializerByte<T>(T message) where T:new()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var msStream = new MemoryStream())
            {
                xmlSerializer.Serialize(msStream, message);
                return msStream.ToArray();
            }
        }
        public string SerializerXmlString<T>(T message) where T : class, new()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var msStream = new StringWriter())
            {
                xmlSerializer.Serialize(msStream, message);
                return msStream.ToString();
            }
        }
        public T Deserializer<T>(byte[] bytes) where T : class, new()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var msStream = new MemoryStream(bytes))
            {
                return xmlSerializer.Deserialize(msStream) as T;
            }
        }
    }
}
