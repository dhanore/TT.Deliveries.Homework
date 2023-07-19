using TT.Deliveries.Data.Dto;

namespace TT.Deliveries.Application.Features.UserFeatures
{
    public sealed record UpdateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;    
    }
}
