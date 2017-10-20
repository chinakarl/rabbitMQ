using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQClient.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQClient.ConfigCommon;
namespace RabbitMQClient
{
    public delegate void ActionHandler(EventMessageResult eventMessageResult);
    
    public class RabbitMQClient:IRabbitMQClient
    {
        private static readonly RabbitMQClient _instance = new RabbitMQClient();
        public RabbitMQContext Context { get; set; }
        private ActionHandler _actionMessage;
        private static IConnection _iConnection = null;
        
        static RabbitMQClient()
        {
            _iConnection = CreateConnection();
        }
        public static  IRabbitMQClient Instance
        {
           get { return _instance; }
        }
        public event ActionHandler ActionHandlerMessage
        {
            add {
                if (_actionMessage.IsNull())
                    _actionMessage += value;
            }
            remove {
                if (_actionMessage.IsNotNull())
                    _actionMessage -= value;
            }
        }
        public void Queueing()
        {
            Task.Factory.StartNew(QueueListen);
        }
        static IConnection CreateConnection()
        {
            //获取配置信息
            var result = ConfigFactory.Instance.GetAppSetting();
            //创建连接工厂
            var confactory = new ConnectionFactory
            {
                HostName = result.Host,
                UserName = result.QName,
                Password = result.QPassword,
                RequestedHeartbeat = 60,//心跳超时时间
                AutomaticRecoveryEnabled = true//自动重连
            };
            return confactory.CreateConnection();//创建连接
        }
        private void QueueListen()
        {
            Context.ListenConnection = _iConnection;
            Context.ListenConnection.ConnectionShutdown += (o, e) =>
            {
                throw new Exception(e.ReplyText);
            };//连接关闭,停止，断开时
            Context.ListenCannel = RabbitMQClientFactory.Instance.CreateModel(Context.ListenConnection);
            var customer = new EventingBasicConsumer(Context.ListenCannel);
        }
        public void constomer_Recevied(object o, BasicDeliverEventArgs e)
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
                    //未消费，重新放入消费队列
                    Context.ListenCannel.BasicReject(e.DeliveryTag, false);
                }
                else if (Context.ListenCannel.IsClosed.IsFalse())
                {
                    Context.ListenCannel.BasicAck(e.DeliveryTag, false);
                }
            }
            catch (Exception)
            {
                throw new Exception("消费者错误");
            }
        }
        /// <summary>
        /// 释放连接
        /// </summary>
        public void Dispoed()
        {
            if (Context.SendConnection.IsNull())
                return;
            if (Context.SendConnection.IsOpen)
                Context.SendConnection.Close();
            Context.SendConnection.Dispose();
         }
    }
}

