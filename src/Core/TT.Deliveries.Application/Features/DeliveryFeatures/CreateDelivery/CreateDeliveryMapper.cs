using AutoMapper;
using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public sealed class CreateDeliveryMapper : Profile
{
    public CreateDeliveryMapper()
    {
        CreateMap<CreateDeliveryRequest, Delivery>();
    }
}
