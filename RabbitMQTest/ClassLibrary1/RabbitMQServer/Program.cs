using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace RabbitMQServer
{
    class Program
    {
        private readonly static ConnectionFactory rabbitMqFactory = new ConnectionFactory() { HostName = "localhost" };
        private static string QueueName = "zhxtest.queue";
        private static string ExchangeName = "zhxtest.exchange";
        static void Main(string[] args)
        {
            using (IConnection conn = rabbitMqFactory.CreateConnection())
            using (IModel channel = conn.CreateModel())
            {
                BasicGetResult msgResponse = channel.BasicGet(QueueName,autoAck:true);
                var consumer = new EventingBasicConsumer(channel);
                var message = Encoding.UTF8.GetString(msgResponse.Body);
                Console.WriteLine(" [x] Received {0}", message);
                //channel.BasicConsume(queue: QueueName,autoAck:true,consumer:consumer);
            }
            Console.ReadLine();
        }
    }
}
