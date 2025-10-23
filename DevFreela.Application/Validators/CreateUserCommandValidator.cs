using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("O {PropertyName} é obrigatório.")
                .MaximumLength(255).WithMessage("O {PropertyName} não pode ultrapassar {MaxLength} caracteres.")
                .Must(ContainAtLeastTwoNames).WithMessage("É obrigatório informar nome e sobrenome.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.")
                .MaximumLength(255).WithMessage("E-mail não pode ultrapassar {MaxLength} caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A {PropertyName} é obrigatória.")
                .Must(ValidPassword).WithMessage("A {PropertyName} deve conter pelo menos 8 caracteres, incluindo letra maiúscula, minúscula, número e caractere especial.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("A {PropertyName} é orbigatória.")
                .Must(BeAValidAge).WithMessage("O usuário deve ter pelo menos 18 anos.");
        }

        private bool ValidPassword(string password)
        {
            var regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }

        private bool ContainAtLeastTwoNames(string fullName)
        {
            var fullNameSplit = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return fullNameSplit.Length > 1;
        }

        private bool BeAValidAge(DateTime birthDate)
        {
            var age = DateTime.Now.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Now.Date.AddYears(age)) age--;
            return age >= 18;
        }
    }
}
