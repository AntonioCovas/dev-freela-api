using DevFreela.Application.Commands.UpdateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("O identificador do usuário é inválido.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("O {PropertyName} é obrigatório.")
                .MaximumLength(255).WithMessage("O {PropertyName} não pode ultrapassar {MaxLength} caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("A {PropertyName} é orbigatória.")
                .Must(BeAValidAge).WithMessage("O usuário deve ter pelo menos 18 anos.");
        }

        private bool BeAValidAge(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
