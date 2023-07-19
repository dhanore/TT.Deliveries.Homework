using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT.Deliveries.Application.Features.DeliveryFeatures;

namespace TT.Deliveries.Scheduler.Functions
{
    public class ExpireDeliveryFunction
    {
        IDeliveryServices services;
        ILogger<ExpireDeliveryFunction> log;
        public ExpireDeliveryFunction(IDeliveryServices services, ILogger<ExpireDeliveryFunction> log)
        {
            this.services = services;
            this.log = log;
        }

        [FunctionName("ExpireDeliveryFunction")]
        public async Task Run([ServiceBusTrigger("%ExpireQueueName%", Connection = "QueueConnection")] string queueMessage,
            CancellationToken ct)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {queueMessage}");
            try
            {
                var getDelivery = await services.getDeliveryById(queueMessage);
                if ((getDelivery.State == Data.Dto.DeliveryState.Created || getDelivery.State == Data.Dto.DeliveryState.Approved)
                    && getDelivery.AccessWindow.EndTime < DateTime.UtcNow)
                {
                    await services.updateDelivery(getDelivery.Id, new UpdateDeliveryRequest()
                    {
                        State = Data.Dto.DeliveryState.Expired
                    }, ct);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Event message: {queueMessage}.",
                    ex);
            }
        }
    }
}
