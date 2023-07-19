using AutoMapper;
using TT.Deliveries.Data.Dto;

namespace TT.Deliveries.Application.Features.UserFeatures.CreateUser
{
    public sealed class LoginUserMapper : Profile
    {
        public LoginUserMapper()
        {
            CreateMap<LoginUserRequest, User>();
            CreateMap<User, LoginUserResponse>();
        }
    }
}
