using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public sealed record CreateDeliveryRequest
{
    public DeliveryState State { get; set; } = DeliveryState.Created;
    public AccessWindow? AccessWindow { get; set; }
    public Recipient? Recipient { get; set; }
    public Order? Order { get; set; }
}
