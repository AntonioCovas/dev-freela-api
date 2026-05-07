using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Projects;

public class DeleteProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly DeleteProjectCommandHandler _handler;

    public DeleteProjectCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _handler = new DeleteProjectCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenExistingProject_WhenHandling_ThenShouldFetchProject_AndCallDeleteAsync()
    {
        // Arrange
        Project project = new("Projeto para Deletar", "Descrição", 1, 2, 2000m);
        DeleteProjectCommand command = new(1);

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id))
            .ReturnsAsync(project);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _projectRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
        _projectRepositoryMock.Verify(r => r.DeleteAsync(project), Times.Once);
    }
}
