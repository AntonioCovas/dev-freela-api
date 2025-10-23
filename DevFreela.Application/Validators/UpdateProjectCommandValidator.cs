using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O identificador do projeto é inválido.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O {PropertyName} do projeto é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O {PropertyName} do projeto não pode ter mais que {MaxLength} caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A {PropertyName} do projeto é obrigatória.")
                .MinimumLength(10)
                .WithMessage("A {PropertyName} deve ter pelo menos {MinLength} caracteres.")
                .MaximumLength(2000)
                .WithMessage("A {PropertyName} não pode ultrapassar {MaxLength} caracteres.");

            RuleFor(x => x.TotalCost)
                .GreaterThan(0)
                .WithMessage("O {PropertyName} deve ser maior que zero.")
                .LessThanOrEqualTo(1_000_000)
                .WithMessage("O {PropertyName} não pode ultrapassar R$ {ComparisonValue:N2}.");
        }
    }
}
