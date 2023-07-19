namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public interface IDeliveryServices
{
    public Task<GetDeliveryResponse> createDelivery(CreateDeliveryRequest request, CancellationToken cancellationToken);
    public Task<List<GetDeliveryResponse>> getAllDelivery();
    public Task<GetDeliveryResponse> getDeliveryById(string id);
    public Task updateDelivery(string id, UpdateDeliveryRequest request, CancellationToken cancellationToken);
    public Task deleteDelivery(string id, CancellationToken cancellationToken);
}
