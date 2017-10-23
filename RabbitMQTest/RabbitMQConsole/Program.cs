using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQClient;
namespace RabbitMQConsole
{
    class Program
    {
        private readonly static ConnectionFactory rabbitMqFactory = new ConnectionFactory() { HostName = "localhost" };
        private static string QueueName = "zhxtest.queue";
        private static string ExchangeName = "zhxtest.exchange";
        static void Main(string[] args)
        {
            //using (IConnection conn = rabbitMqFactory.CreateConnection())
            //using (IModel channel = conn.CreateModel())
            //{
            //    channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);

            //    channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            //    channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
            //    string message = "hellow word";
            //    var body = Encoding.UTF8.GetBytes(message);
            //    channel.BasicPublish(ExchangeName, QueueName, null, body);
            //    Console.WriteLine(message);
            //}
            //IConnection con=   RabbitMQClientFactory.Instance.CreateConnection();
            //RabbitMQClientFactory.Instance.CreateModel(con);


            Console.ReadLine();
        }
        private static void Listening()
        {
            //RabbitMQClient.RabbitMQClient.Instance.ActionHandlerMessage += mqClient_ActionEventMessage;
           // RabbitMQClient.RabbitMQClient.Instance.Queueing();
        }

        //private static void mqClient_ActionEventMessage(EventMessageResult result)
        //{
        //    if (result.EventMessageBytes.EventMessageMarkcode =="")
        //    {
        //        var message =
        //            MessageSerializerFactory.CreateMessageSerializerInstance()
        //                .Deserialize<UpdatePurchaseOrderStatusByBillIdMqContract>(result.MessageBytes);

        //        result.IsOperationOk = true; //处理成功

        //        Console.WriteLine(message.ModifiedBy);
        //    }
        //}

        //private static void SendEventMessage()
        //{
        //    for (var i = 1; i < 10000; i++)
        //    {
        //        var originObject = new UpdatePurchaseOrderStatusByBillIdMqContract()
        //        {
        //            UpdatePurchaseOrderStatusType = 1,
        //            RelationBillType = 10,
        //            RelationBillId = 10016779,
        //            UpdateStatus = 30,
        //            ModifiedBy = i
        //        };

        //        var sendMessage =
        //            EventMessageFactory.CreateEventMessageInstance(originObject, MessageTypeConst.ZgUpdatePurchaseStatus);

        //        RabbitMQClientC.Instance.TriggerEventMessage(sendMessage, "zhxtest.exchange", "zhxtest.queue");

        //        Console.WriteLine(i);
        //    }
        //}
    }
    public class UpdatePurchaseOrderStatusByBillIdMqContract
    {
        public int UpdatePurchaseOrderStatusType;
        public int RelationBillType;
        public int RelationBillId;
        public int UpdateStatus;
        public int ModifiedBy;
    }
}
