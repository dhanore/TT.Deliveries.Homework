using AutoMapper;
using System.Linq.Expressions;
using TT.Deliveries.Application.Common;
using TT.Deliveries.Application.Repositories;
using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public class DeliveryServices : IDeliveryServices
{
    private readonly IDeliveryRepository deliveryRepository;
    private readonly IMapper mapper;
    private readonly ValidationBehavior<CreateDeliveryRequest, Delivery> createValidators;
    private readonly ValidationBehavior<UpdateDeliveryRequest, Delivery> updateValidators;

    public DeliveryServices(IDeliveryRepository deliveryRepository, IMapper mapper,
        ValidationBehavior<CreateDeliveryRequest, Delivery> createValidators,
        ValidationBehavior<UpdateDeliveryRequest, Delivery> updateValidators)
    {
        this.deliveryRepository = deliveryRepository;
        this.mapper = mapper;
        this.createValidators = createValidators;
        this.updateValidators = updateValidators;
    }

    public async Task<GetDeliveryResponse> createDelivery(CreateDeliveryRequest request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<Delivery>(request);
        user = createValidators.Handle(request, user);
        var result = await deliveryRepository.InsertOne(user);
        return mapper.Map<GetDeliveryResponse>(result);
    }

    public async Task updateDelivery(string id, UpdateDeliveryRequest request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<Delivery>(request);
        await updateRequest(id, request);
    }

    private async Task updateRequest(string id, UpdateDeliveryRequest request)
    {
        if (!string.IsNullOrEmpty(request.State.ToString()))
            await deliveryRepository.UpdateOne((_) => _.Id, id, (_) => _.State, request.State);
    }

    public async Task deleteDelivery(string id, CancellationToken cancellationToken)
    {
        await deliveryRepository.Delete((_) => _.Id, id);
    }

    public async Task<List<GetDeliveryResponse>> getAllDelivery()
    {
        var userList = await deliveryRepository.GetAll();
        var listmap = mapper.Map<GetDeliveryResponse[]>(userList.ToArray());

        return listmap.ToList();
    }

    public async Task<GetDeliveryResponse> getDeliveryById(string id)
    {
        var filter = new Dictionary<Expression<Func<Delivery, object>>, object>
        {
           {_=>_.Id,id }
        };
        var delivery = await deliveryRepository.GetByParam(filter);
        return mapper.Map<GetDeliveryResponse>(delivery);
    }
}
