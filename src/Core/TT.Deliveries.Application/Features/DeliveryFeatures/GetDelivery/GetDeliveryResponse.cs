using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public sealed record GetDeliveryResponse
{
    public string Id { get; set; }
    public DeliveryState State { get; set; }
    public AccessWindow AccessWindow { get; set; }
    public Recipient Recipient { get; set; }
    public Order Order { get; set; }
}
