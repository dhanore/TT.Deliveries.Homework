using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public sealed record CreateDeliveryResponse
{
    public string Id { get; }
    public DeliveryState State { get; }
    public AccessWindow AccessWindow { get; }
    public Recipient Recipient { get; }
    public Order Order { get; }
}
