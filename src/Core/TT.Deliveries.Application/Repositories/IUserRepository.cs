using TT.Deliveries.Data.Dto;

namespace TT.Deliveries.Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmail(string email, CancellationToken cancellationToken);
    Task<User> ValidateUser(string email, string password, CancellationToken cancellationToken);
}
