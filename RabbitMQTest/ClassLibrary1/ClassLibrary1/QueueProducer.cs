using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQClient.ConfigCommon;
using RabbitMQClient.Common;
namespace RabbitMQClient
{
     //delegate void ActionHandler(EventMessageResult messageResult);
    public class QueueProducer :IDisposable
    {
        private static readonly QueueProducer queueProducer;
        private ActionHandler _actionMessage;
        private static IConnection _IConnection = null;
        private static IModel _IModel = null;
        public MessageContext Context { get; set; }
        public QueueProducer()
        {
        }
        public QueueProducer(string exchangeName, string queueName)
        {
            Init();
        }
        public QueueProducer(string exchangeName)
            :this(exchangeName,"")
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
        private void Init()
        {
            Context = new MessageContext()
            {
                ListenQueueName = "zhxtest.queue"
            };
            _IConnection =ServerManger.Instance.CreateConnectionFactory().CreateConnection();
            _IModel = _IConnection.CreateModel();

        }
        public void OnListening()
        {
            Init();
            Task.Factory.StartNew(ListenInit);
        }
        public void ListenInit()
        {
            Context.ListenConnection = _IConnection;
            //记录监听日志
            Context.ListenConnection.ConnectionShutdown += (e, o) =>
            {
                throw new Exception();
            };
            Context.ListenCannel = _IModel;
            var consumer = new EventingBasicConsumer(Context.ListenCannel);
            consumer.Received += consumer_received;
            try
            {
                Context.ListenCannel.BasicConsume(Context.ListenQueueName, false, consumer);
            }
            catch (Exception)
            {
                throw  new Exception("接受信息出错");
            }
            
        }
        public void consumer_received(object o,BasicDeliverEventArgs e)
        {
            try
            {
                var result = EventMessage.BuildMessageResult(e.Body);
                if (_actionMessage.IsNotNull())
                {
                    _actionMessage(result);
                }
                if (result.IsOperationOk.IsFalse())
                {
                    //如果没有消费，重新放回队列
                    Context.ListenCannel.BasicReject(e.DeliveryTag, true);
                }
                else if (Context.ListenCannel.IsClosed.IsFalse())
                {
                    //如果没有关闭，返回状态
                    Context.ListenCannel.BasicAck(e.DeliveryTag, true);
                }
            }
            catch (Exception)
            {

                throw new Exception();
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
            if(!Context.SendCannel.IsClosed)
               Context.SendCannel.Close();
            if (Context.SendConnection.IsOpen)
                Context.SendConnection.Close();
        }
    }
}
