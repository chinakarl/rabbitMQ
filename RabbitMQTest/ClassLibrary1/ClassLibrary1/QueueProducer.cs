using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQClient.ConfigCommon;

namespace RabbitMQClient
{
     //delegate void ActionHandler(EventMessageResult messageResult);
    public class QueueProducer :IDisposable
    {
        private static readonly QueueProducer queueProducer;
        private ActionHandler _actionMessage;
        private static IConnection _IConnection = null;
        private static IModel _IModel = null;
        public RabbitMQContext Context { get; set; }
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
            var result = ConfigFactory.Instance.GetAppSetting();
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName= result.Host,
                UserName=result.QName,
                Password=result.QPassword,
                RequestedHeartbeat=60,//心跳超时时间
                AutomaticRecoveryEnabled=true//自动重连
            };
            _IConnection = connectionFactory.CreateConnection();
            _IModel = _IConnection.CreateModel();
        }
        public void OnListening()
        {
            Task.Factory.StartNew(ListenInit);
        }
        public void ListenInit()
        {
            Context.SendConnection = _IConnection;
            //记录监听日志
            Context.SendConnection.ConnectionShutdown += (e, o) =>
            {
                throw new Exception();
            };
            Context.SendCannel = _IModel;
            var consumer = new EventingBasicConsumer(Context.SendCannel);
            consumer.Received += consumer_received;
        }
        public void consumer_received(object o,BasicDeliverEventArgs e)
        {
            try
            {
                var result = EventMessage.BuildMessageResult(e.Body);

            }
            catch (Exception)
            {

                throw;
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
