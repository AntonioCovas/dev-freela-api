using DevFreela.Application.Commands.FinishProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class FinishProjectCommandValidator : AbstractValidator<FinishProjectCommand>
    {
        public FinishProjectCommandValidator()
        {
            RuleFor(x => x.Id)
              .GreaterThan(0)
              .WithMessage("O identificador do projeto é inválido.");
        }
    }
}
