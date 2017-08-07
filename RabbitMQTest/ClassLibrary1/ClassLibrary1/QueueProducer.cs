using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class QueueProducer :IDisposable
    {
        private static readonly QueueProducer queueProducer;
        public QueueProducer()
        {
        }
        static QueueProducer()
        {
            queueProducer = new QueueProducer();
        }
        public static QueueProducer Instance {
            get {
                return queueProducer; 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">原始强类型对象</typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        public static EventMessageResult EventMessageResultCreateInstance<T>(T origObject)
        {
            var result = new EventMessageResult
            {
                CreateTime = DateTime.Now,
            };
            
            return result;
        }
        public void Dispose() {

        }
    }
}
