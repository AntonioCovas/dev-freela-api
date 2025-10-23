using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("O {PropertyName} é obrigatório.")
                .MaximumLength(50)
                .WithMessage("O {PropertyName} do projeto não pode ter mais que {MaxLength} caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("A {PropertyName} é obrigatória.")
                .MaximumLength(200)
                .WithMessage("A {PropertyName} não pode ter mais que {MaxLength} caracteres.");

            RuleForEach(x => x.Comments)
                .Where(x => !string.IsNullOrEmpty(x.Content))
                .ChildRules(comment =>
                {
                    comment.RuleFor(x => x.Content)
                        .MaximumLength(500)
                        .WithMessage("O {PropertyName} do comentário não pode ter mais que {MaxLength} caracteres.");
                });

            RuleFor(x => x.TotalCost)
                .GreaterThan(0)
                .WithMessage("O {PropertyName} não pode ser menor ou igual a 0.");
        }
    }
}
