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
        public MessageContext Context { get; set; }
        private ActionHandler _actionMessage;
        private static IConnection _iConnection = null;
        
        static RabbitMQClient()
        {

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
        private void QueueListen()
        {
            Context.ListenConnection = _iConnection;
            //留着输出日志，监控当前侦听状况
            Context.ListenConnection.ConnectionShutdown += (o, e) =>
            {
                throw new Exception(e.ReplyText);
            };//连接关闭,停止，断开时
            var customer = new EventingBasicConsumer(Context.ListenCannel);
            customer.Received += constomer_Recevied;
            try
            {
                Context.ListenCannel.BasicConsume(Context.ListenQueueName,false,customer);
            }
            catch (Exception)
            {
                throw new Exception("监听队列出错");
                throw;
            }
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
        public void Send(EventMessage eventMessage,string exchangeName,string queueName)
        {
            Context.SendConnection = ServerManger.Instance.CreateConnectionFactory().CreateConnection();
            const byte DeliveryTag = 2;
            using (Context.SendConnection)
            {
                var messageSerializer = MessageSerializerFactory.CreateMessageSerializerInstance();
                Context.SendCannel = Context.SendConnection.CreateModel();
                var properties = Context.SendCannel.CreateBasicProperties();
                properties.DeliveryMode = DeliveryTag;
                Context.SendCannel.BasicPublish(exchangeName, queueName, true, properties, messageSerializer.SerializerByte(eventMessage));
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

