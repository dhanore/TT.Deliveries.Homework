using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Repositories;
public interface IDeliveryRepository : IBaseRepository<Delivery>
{
    //Task UpdateState(string id, DeliveryState state, CancellationToken cancellationToken);
}
