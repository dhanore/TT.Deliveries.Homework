using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public sealed record UpdateDeliveryRequest
{
    public DeliveryState State { get; set; }

}
