using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    /// <summary>
    /// 事件消息工厂
    /// </summary>
    public class EventMessageFactory
    {
        public static EventMessage CreateEventMessageInstance<T>(T originObject, string eventMessageMarkcode) where T :class,new()
        {
            var result = new EventMessage
            {
                CreateTime = DateTime.Now,
                EventMessageMarkcode=eventMessageMarkcode
            };
            ///序列化消息
            var bytes = MessageSerializerFactory.CreateMessageSerializerInstance().SerializerByte<T>(originObject);
            result.EventMessageBytes = bytes;
            return result;
        }
    }
}
