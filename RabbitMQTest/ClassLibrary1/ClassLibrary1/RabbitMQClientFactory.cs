using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQClient.ConfigCommon;
using RabbitMQ.Client;
namespace RabbitMQClient
{
    internal class RabbitMQClientFactory
    {
        public static readonly RabbitMQClientFactory Instance = null;
        static RabbitMQClientFactory()
        {
            Instance = new RabbitMQClientFactory();
        }
        public IConnection CreateConnection()
        {
            var result = ConfigFactory.Instance.GetAppSetting();
            var confactory = new ConnectionFactory
            {
                HostName = result.Host,
                UserName = result.QName,
                Password = result.QPassword,
                RequestedHeartbeat = 60,//心跳超时时间
                AutomaticRecoveryEnabled=true//自动重连
            };
            return confactory.CreateConnection();//创建连接
        }
    }
}
