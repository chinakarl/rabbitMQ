using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQClient.Common;
using RabbitMQ.Client;

namespace RabbitMQClient
{
    public delegate void ActionHandler(EventMessageResult eventMessageResult);
    public class RabbitMQClient:IRabbitMQClient
    {
        public RabbitMQContext Context { get; set; }
        private ActionHandler _actionMessage;
        public event ActionHandler ActionHandlerMessage
        {
            add {
                if (_actionMessage.IsNull())
                    _actionMessage += value;
            }
            remove {
                if (_actionMessage.IsNotNull())
                    _actionMessage -= value;
            }
        }
        public void Queueing()
        {
            Task.Factory.StartNew(QueueListen);
        }
        private void QueueListen()
        {
            Context.ListenConnection = RabbitMQClientFactory.Instance.CreateConnection();
        }
    }
}

