using AutoMapper;
using System.Linq.Expressions;
using TT.Deliveries.Application.Common;
using TT.Deliveries.Application.Repositories;
using TT.Deliveries.Data.Dto;

namespace TT.Deliveries.Application.Features.UserFeatures
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ValidationBehavior<CreateUserRequest, User> createValidators;
        private readonly ValidationBehavior<UpdateUserRequest, User> updateValidators;

        public UserServices(IUserRepository userRepository, IMapper mapper,
            ValidationBehavior<CreateUserRequest, User> createValidators, ValidationBehavior<UpdateUserRequest, User> updateValidators)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.createValidators = createValidators;
            this.updateValidators = updateValidators;
        }

        public async Task<GetUserResponse> createUser(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request);
            user = createValidators.Handle(request, user);
            var result = await userRepository.InsertOne(user);
            return mapper.Map<GetUserResponse>(result);
        }

        public async Task updateUser(string id, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request);
            //user = updateValidators.Handle(request, user);
            await updateRequest(id, request);
        }

        private async Task updateRequest(string id, UpdateUserRequest request)
        {
            if (!string.IsNullOrEmpty(request.Password))
                await userRepository.UpdateOne((_) => _.Id, id, (_) => _.Password, request.Password);
            if (!string.IsNullOrEmpty(request.Name))
                await userRepository.UpdateOne((_) => _.Id, id, (_) => _.Name, request.Name);
            if (request.Role != null)
                await userRepository.UpdateOne((_) => _.Id, id, (_) => _.Role, request.Role);
        }

        public async Task deleteUser(string id, CancellationToken cancellationToken)
        {
            await userRepository.Delete((_) => _.Id, id);
        }

        public async Task<List<GetUserResponse>> getAllUser()
        {
            var userList = await userRepository.GetAll();
            var listmap = mapper.Map<GetUserResponse[]>(userList.ToArray());

            return listmap.ToList();
        }

        public async Task<GetUserResponse> getUserById(string id)
        {
            var filter = new Dictionary<Expression<Func<User, object>>, object>
            {
                {_=>_.Id,id }
            };
            var users = await userRepository.GetByParam(filter);
            return mapper.Map<GetUserResponse>(users);
        }

        public async Task<GetUserResponse> validateUser(string email, string password, CancellationToken ct)
        {
            var users = await userRepository.ValidateUser(email, password, ct);
            return mapper.Map<GetUserResponse>(users);
        }
    }
}
