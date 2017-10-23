using RabbitMQ.Client;
using RabbitMQClient.ConfigCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class ServerManger
    {
        private static readonly ServerManger _serverManger = new ServerManger();
        private ServerManger()
        {

        }
        public static ServerManger Instance
        {
            get { return _serverManger; }
        }
        public ConnectionFactory CreateConnectionFactory()
        {
            //读取配置文件，可以将读取配置文件封装
            var result = ConfigFactory.Instance.GetAppSetting();
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = result.Host,
                UserName = result.QName,
                Password = result.QPassword,
                RequestedHeartbeat = 60,//心跳超时时间
                AutomaticRecoveryEnabled = true//自动重连
            };
            return connectionFactory;
        }
    }
}
