using System.Linq.Expressions;
using TT.Deliveries.Application.Repositories;
using TT.Deliveries.Data.Dto;
using TT.Deliveries.Persistence.Context;

namespace TT.Deliveries.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MongoDBContext<User> _) : base(_) { }

        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var filter = new Dictionary<Expression<Func<User, object>>, object>
            {
                {_=>_.Email,email }
            };
            return await dbContext.GetByFilter(filter);
        }

        public async Task<User> ValidateUser(string email, string password, CancellationToken cancellationToken)
        {
            var filter = new Dictionary<Expression<Func<User, object>>, object>
            {
                {_=>_.Email,email },
                {_=>_.Password,password },
            };
            return await dbContext.GetByFilter(filter);
        }
    }
}
