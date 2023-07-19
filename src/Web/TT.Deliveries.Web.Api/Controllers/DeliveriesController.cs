namespace TT.Deliveries.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.Amqp.Framing;
    using Newtonsoft.Json;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using TT.Deliveries.Application.Common;
    using TT.Deliveries.Application.Features.DeliveryFeatures;
    using TT.Deliveries.Domain.Common;

    [Authorize]
    [Route("deliveries")]
    [ApiController]
    [Produces("application/json")]
    public class DeliveriesController : ControllerBase
    {
        IDeliveryServices services;
        private readonly IQueueClient<ScheduleMessage> expireQueueClient;

        public DeliveriesController(IDeliveryServices services, IQueueClient<ScheduleMessage> expireQueueClient)
        {
            this.services = services;
            this.expireQueueClient = expireQueueClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await services.getAllDelivery();
            return new OkObjectResult(JsonConvert.SerializeObject(res));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            var res = await services.getDeliveryById(id);
            if (res.Id == null)
            {
                return NotFound();
            }
            return new OkObjectResult(JsonConvert.SerializeObject(res));
        }

        [Authorize(Roles = "User")]
        [HttpPost] //'Created'
                   //'Expired' - After EndTime reached ServiceBus change status
        public async Task<JsonResult> Create([FromBody] CreateDeliveryRequest model, CancellationToken ct)
        {
            var result = await services.createDelivery(model, ct);

            var messageSequenceNumber = await expireQueueClient.ScheduleMessageAsync(
                                        new ScheduleMessage
                                        {
                                            Id = result.Id,
                                            Message = $"Order : {model.Order.OrderName} - Sender :{model.Order.Sender}"

                                        }, model.AccessWindow.EndTime);

            //TODO - we can save messageSequenceNumber for debug and further detail

            return new JsonResult(new
            {
                statusCode = (int)HttpStatusCode.OK,
                message = JsonConvert.SerializeObject(result)
            });
        }

        //[Route("/deliveries/{id}")]  // Approved , Completed, Cancelled
        [HttpPost("{id}")]
        public async Task<JsonResult> Update(string id, [FromBody] UpdateDeliveryRequest model, CancellationToken ct)
        {
            await services.updateDelivery(id, model, ct);

            return new JsonResult(new
            {
                statusCode = (int)HttpStatusCode.OK,
                message = "updated"
            });
        }


        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(string id, CancellationToken ct)
        {
            await services.deleteDelivery(id, ct);

            return new JsonResult(new
            {
                statusCode = (int)HttpStatusCode.OK,
                message = "deleted"
            });
        }
    }
}
