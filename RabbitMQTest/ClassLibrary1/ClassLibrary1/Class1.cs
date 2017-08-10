using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Client
{
    public class Test
    {
        private readonly ConnectionFactory rabbitMqFactory = new ConnectionFactory() { HostName="localhost" };
        private string QueueName = "zhxtest.queue";//队列名称
        private string ExchangeName = "zhxtest.exchange";//异常名称
        public void Register_durable_Exchange_and_Queue()
        {
            //创建连接
            using (IConnection conn = rabbitMqFactory.CreateConnection())
            using (IModel channel = conn.CreateModel())
            {
                //创建异常队列
                channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);
                //创建队列
                channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                //绑定队列
                channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
                string message = "hellow word";//消息
                var body = Encoding.UTF8.GetBytes(message);//转换成byte[]
                channel.BasicPublish(ExchangeName, QueueName, null, body);//推送队列消息
                Console.WriteLine(message);
            }
            Console.ReadLine();
        }
    }
}
