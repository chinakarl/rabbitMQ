using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class EventMessageResult
    {
        public DateTime CreateTime { get; set; }
        public string EventMessageMarkCode { get; set; }
        public byte[] EventMessageBytes { get; set; }
    }
}
