using DevFreela.Application.ViewModels;
using DevFreela.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UserLogin
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
       
        public UserLoginCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        async Task<UserLoginViewModel> IRequestHandler<UserLoginCommand, UserLoginViewModel>.Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email, request.Password);
            if (user == null) return null;

            var isValid = _authService.VerifyPassword(user.Password, request.Password);
            if (!isValid) return null; // será que essa validação não deveria ser no Fluent??

            var token = _authService.GenerateJwtToken(user.Email, user.Role);
            return new UserLoginViewModel(user.Email, token);
        }
    }
}
