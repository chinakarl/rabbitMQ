using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RabbitMQClient.ConfigCommon
{
    public class ConfigFactory
    {
        public static readonly ConfigFactory Instance=null;
         static ConfigFactory()
        {
            Instance = new ConfigFactory();
        }
        public ConfigModel GetAppSetting()
        {
            ConfigModel result = new ConfigModel();
            result.Host = ConfigurationSettings.AppSettings["MqHost"];
            result.QName = ConfigurationSettings.AppSettings["MqUserName"];
            result.QPassword = ConfigurationSettings.AppSettings["MqPassword"];
            result.NormalQ = ConfigurationSettings.AppSettings["MqListenQueueName"];
            return result;
        }
    }
}
