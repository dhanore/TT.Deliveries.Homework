namespace TT.Deliveries.Data.Dto;
public sealed class User : BaseEntity
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
}