using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public delegate void ActionHandler(EventMessageResult eventMessageResult);
    public class RabbitMQClient:IRabbitMQClient
    {
        public RabbitMQContext Context { get; set; }

    }
}
