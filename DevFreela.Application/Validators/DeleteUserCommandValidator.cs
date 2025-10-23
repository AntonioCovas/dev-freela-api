using DevFreela.Application.Commands.DeleteUser;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
              .GreaterThan(0)
              .WithMessage("O identificador do usuário é inválido.");
        }
    }
}
