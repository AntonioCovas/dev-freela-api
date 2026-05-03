using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Interfaces;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var newUser = new User(request.FullName, request.Email, request.BirthDate, request.Password, request.Role);
            var newUserId = await _userRepository.AddAsync(newUser);
            return newUserId;
        }
    }
}
