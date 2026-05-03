using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            request.Password = _authService.HashPassword(request.Password);
            var newUser = new Core.Entities.User(request.FullName, request.Email, request.BirthDate, request.Password, request.Role);
            var newUserId = await _userRepository.AddAsync(newUser);
            return newUserId;
        }
    }
}
