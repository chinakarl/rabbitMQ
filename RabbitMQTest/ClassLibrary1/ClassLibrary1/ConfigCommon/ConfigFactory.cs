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
            result.Host = ConfigurationSettings.AppSettings["Host"];
            result.QName = ConfigurationSettings.AppSettings["QName"];
            result.QPassword = ConfigurationSettings.AppSettings["QPassword"];
            result.NormalQ = ConfigurationSettings.AppSettings["NormalQ"];
            return result;
        }
    }
}
