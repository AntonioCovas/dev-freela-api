using DevFreela.Application.Commands;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Commands.UpdateUser;
using FluentValidation;

namespace DevFreela.API
{
    public static class FluentValidationConfig
    {
        public static void ConfigureDisplayNames()
        {
            ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) =>
            {
                if (member == null) return null;

                var displayNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { nameof(CreateCommentCommand.Content), "Conteúdo" },
                { nameof(CreateProjectCommand.Title), "Título" },
                { nameof(CreateProjectCommand.Description), "Descrição" },
                { nameof(CreateProjectCommand.TotalCost), "Custo total" },
                { nameof(CreateUserCommand.FullName), "Nome completo" },
                { nameof(CreateUserCommand.Email), "E-mail" },
                { nameof(CreateUserCommand.Password), "Senha" },
                { nameof(CreateUserCommand.BirthDate), "Data de aniversário" },
                { nameof(UpdateProjectCommand.Title), "Título" },
                { nameof(UpdateProjectCommand.Description), "Descrição" },
                { nameof(UpdateUserCommand.FullName), "Nome completo" },
                { nameof(UpdateUserCommand.BirthDate), "Data de aniversário" },
            };

                return displayNames.TryGetValue(member.Name, out var friendlyName)
                    ? friendlyName
                    : member.Name;
            };
        }
    }

}
