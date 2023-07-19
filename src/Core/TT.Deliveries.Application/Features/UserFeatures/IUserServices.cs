using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT.Deliveries.Application.Features.UserFeatures
{
    public interface IUserServices
    {
        public Task<GetUserResponse> createUser(CreateUserRequest request, CancellationToken cancellationToken);
        public Task<List<GetUserResponse>> getAllUser();
        public Task<GetUserResponse> getUserById(string id);
        public Task updateUser(string id, UpdateUserRequest request, CancellationToken cancellationToken);
        public Task deleteUser(string id, CancellationToken cancellationToken);
        public Task<GetUserResponse> validateUser(string email, string password, CancellationToken cancellationToken);

    }
}
