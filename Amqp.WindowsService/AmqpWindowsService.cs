using RabbitMQClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Amqp.WindowsService
{
    public partial class AmqpWindowsService : ServiceBase
    {
        public AmqpWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Listening();
        }

        protected override void OnStop()
        {
        }
        private static void Listening()
        {
            RabbitMQClient.RabbitMQClient.Instance.ActionHandlerMessage += mqClient_ActionEventMessage;
            RabbitMQClient.RabbitMQClient.Instance.Queueing();
        }

        private static void mqClient_ActionEventMessage(EventMessageResult result)
        {
            if (result.EventMessageBytes.EventMessageMarkcode == "")
            {
                var message =
                    MessageSerializerFactory.CreateMessageSerializerInstance()
                        .Deserializer<UpdatePurchaseOrderStatusByBillIdMqContract>(result.MessageBytes);

                result.IsOperationOk = true; //处理成功

                Console.WriteLine(message.ModifiedBy);
            }
        }
        public class UpdatePurchaseOrderStatusByBillIdMqContract
        {
            public int UpdatePurchaseOrderStatusType;
            public int RelationBillType;
            public int RelationBillId;
            public int UpdateStatus;
            public int ModifiedBy;
        }
    }
}
