using TT.Deliveries.Application.Repositories;
using TT.Deliveries.Data.Dto;
using TT.Deliveries.Persistence.Context;
namespace TT.Deliveries.Persistence.Repositories;
public class DeliveryRepository : BaseRepository<Delivery>, IDeliveryRepository
{
    public DeliveryRepository(MongoDBContext<Delivery> _) : base(_) { }
}
