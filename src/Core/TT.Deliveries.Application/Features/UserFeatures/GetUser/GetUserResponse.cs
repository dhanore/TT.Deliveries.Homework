using TT.Deliveries.Data.Dto;

namespace TT.Deliveries.Application.Features.UserFeatures;
public sealed record GetUserResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.User;

}
