using AutoMapper;
using TT.Deliveries.Data.Dto;

namespace TT.Deliveries.Application.Features.UserFeatures.CreateUser
{
    public sealed class GetUserMapper : Profile
    {
        public GetUserMapper()
        {
            CreateMap<User, GetUserResponse>();
        }
    }
}
