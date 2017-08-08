using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient.ConfigCommon
{
    [Serializable]
    public class ConfigModel
    {
        /// <summary>
        /// RabbitMQ服务器(IP)
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string QName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string QPassword { get; set; }
        /// <summary>
        /// 默认走的q
        /// </summary>
        public string NormalQ { get; set; }
    }
}
