using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RabbitMQClient
{
    public interface IRabbitMQClient
    {
        MessageContext Context { get; set; }
        event ActionHandler ActionHandlerMessage;

        void Queueing();
    }
}
