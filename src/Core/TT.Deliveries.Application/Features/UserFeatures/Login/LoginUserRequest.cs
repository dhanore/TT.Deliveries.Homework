namespace TT.Deliveries.Application.Features.UserFeatures;
public sealed record LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
