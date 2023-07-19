using AutoMapper;
using TT.Deliveries.Data.Dto;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public sealed class UpdateDeliveryMapper : Profile
{
    public UpdateDeliveryMapper()
    {
        CreateMap<UpdateDeliveryRequest, Delivery>();
    }
}
