using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT.Deliveries.Domain.Common
{
    public class SchedulerSettings
    {
        public string QueueConnection { get; set; }
        public string ExpireDeliveryQueueName { get; set; }
    }
}
