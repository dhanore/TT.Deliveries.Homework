using AutoMapper;
using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public sealed class GetDeliveryMapper : Profile
{
    public GetDeliveryMapper()
    {
        CreateMap<Delivery, GetDeliveryResponse>();
    }
}
