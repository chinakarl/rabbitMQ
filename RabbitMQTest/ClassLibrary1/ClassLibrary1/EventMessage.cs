using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class EventMessage
    {
        /// <summary>
        /// 消息的标记码。
        /// </summary>
        public string EventMessageMarkcode { get; set; }

        /// <summary>
        /// 消息的序列化字节流。
        /// </summary>
        public byte[] EventMessageBytes { get; set; }

        /// <summary>
        /// 创建消息的时间。
        /// </summary>
        public DateTime CreateTime { get; set; }
        internal static EventMessageResult BuildMessageResult(byte[] bytes)
        {
            var eventMessage = MessageSerializerFactory.CreateMessageSerializerInstance().Deserializer<EventMessage>(bytes);
            var result = new EventMessageResult
            {
                MessageBytes = eventMessage.EventMessageBytes,
                EventMessageBytes = eventMessage
            };
            return result;
        }
    }
}
