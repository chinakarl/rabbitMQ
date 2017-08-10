using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQClient.ConfigCommon;
using RabbitMQ.Client;
namespace RabbitMQClient
{
    /// <summary>
    /// RabbitMQ 工厂方法
    /// </summary>
    internal class RabbitMQClientFactory
    {
        public static readonly RabbitMQClientFactory Instance = null;
        static RabbitMQClientFactory()
        {
            Instance = new RabbitMQClientFactory();
        }
        public IConnection CreateConnection()
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
                AutomaticRecoveryEnabled=true//自动重连
            };
            return confactory.CreateConnection();//创建连接
        }
        public IModel CreateModel(IConnection connection)
        {
            return connection.CreateModel();
        }
    }
}
