using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using TT.Deliveries.Domain.Common;

namespace TT.Deliveries.Application.Common
{
    public class QueueClient<T> : IQueueClient<T> where T : ScheduleMessage
    {
        private readonly ILogger<QueueClient<T>> logger;
        private readonly ServiceBusSender sender;

        public QueueClient(ServiceBusSender sender, ILogger<QueueClient<T>> logger)
        {
            this.sender = sender;
            this.logger = logger;
        }

        public async Task<long> ScheduleMessageAsync(T notificationMessage, DateTime scheduledEnqueueTime)
        {
            var messageBody = JsonConvert.SerializeObject(notificationMessage);
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
            return await sender.ScheduleMessageAsync(message, scheduledEnqueueTime);
        }

        public async Task CancelScheduledMessageAsync(long messageSequenceNumber)
        {
            try
            {
                await sender.CancelScheduledMessageAsync(messageSequenceNumber);
            }
            catch (Exception ex)
            {
                logger.LogWarning("Unable to cancel scheduled message with sequence number {0} with exception {1}",
                    messageSequenceNumber, ex);
            }
        }
    }
}
