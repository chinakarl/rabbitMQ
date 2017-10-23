using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;
namespace RabbitMQClient
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class MessageContext
    {
        //数据
        public virtual object Data { get; set; }
        /// <summary>
        /// 原始数据
        /// </summary>
        public virtual byte[] RawData { get; set; }
        /// <summary>
        /// 发送链接Connection
        /// </summary>
        public IConnection SendConnection { get; set; }
        /// <summary>
        /// 发送Cannel
        /// </summary>
        public IModel SendCannel { get; set; }
        /// <summary>
        /// 侦听链接Connection
        /// </summary>
        public IConnection ListenConnection { get; set; }
        /// <summary>
        /// 侦听Cannel
        /// </summary>
        public IModel ListenCannel { get; set; }
        /// <summary>
        /// 队列名称
        /// </summary>
        public string ListenQueueName { get; set; }
        /// <summary>
        /// 异常队列名称
        /// </summary>
        public string ExchangeQueueName { get; set; }
        /// <summary>
        /// 队列数
        /// </summary>
        public int QueueCount { get; set; }
    }
}
