using AutoMapper;
using TT.Deliveries.Data.Dto;

namespace TT.Deliveries.Application.Features.UserFeatures.CreateUser
{
    public sealed class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<CreateUserRequest, User>();
        }
    }
}
