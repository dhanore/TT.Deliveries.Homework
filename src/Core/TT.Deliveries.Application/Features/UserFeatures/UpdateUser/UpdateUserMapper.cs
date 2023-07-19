using AutoMapper;
using TT.Deliveries.Data.Dto;

namespace TT.Deliveries.Application.Features.UserFeatures.CreateUser
{
    public sealed class UpdateUserMapper : Profile
    {
        public UpdateUserMapper()
        {
            CreateMap<UpdateUserRequest, User>();
        }
    }
}
