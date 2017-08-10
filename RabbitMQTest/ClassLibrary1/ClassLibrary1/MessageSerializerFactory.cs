using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    /// <summary>
    /// 消息序列化工厂方法
    /// </summary>
    public class MessageSerializerFactory
    {
        public static IMessageSerializer CreateMessageSerializerInstance()
        {
            return new MessageSerializer();
         }
    }
}
