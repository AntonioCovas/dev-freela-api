using DevFreela.Application.Commands.CreateComment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("O conteúdo do comentário é obrigatório.")
                .MaximumLength(500)
                .WithMessage("O comentário não pode exceder 500 caracteres.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("O ID do projeto é inválido.");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("O ID do usuário é inválido.");
        }
    }
}
