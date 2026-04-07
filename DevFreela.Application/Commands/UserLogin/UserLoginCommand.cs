using DevFreela.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UserLogin
{
    public class UserLoginCommand : IRequest<UserLoginViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
