using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace RabbitMQConsole
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
                channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);

                channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
                string message = "hellow word";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(ExchangeName, QueueName, null, body);
                Console.WriteLine(message);
            }
            Console.ReadLine();
        }
    }
}
