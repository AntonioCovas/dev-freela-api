using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Commands.Project.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
                _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var newProject = new Core.Entities.Project(request.Title, request.Description, request.IdClient,
                                         request.IdFreelancer, request.TotalCost);

            int newProjectId = await _projectRepository.AddAsync(newProject);
            return newProjectId;
        }
    }
}
