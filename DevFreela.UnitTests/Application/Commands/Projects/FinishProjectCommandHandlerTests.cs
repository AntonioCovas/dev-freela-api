using DevFreela.Application.Commands.Project.FinishProject;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Projects;

public class FinishProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly FinishProjectCommandHandler _handler;

    public FinishProjectCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _handler = new FinishProjectCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenExistingProject_WhenHandling_ThenShouldCallFinishAsync()
    {
        // Arrange
        FinishProjectCommand command = new(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _projectRepositoryMock.Verify(r => r.FinishAsync(command.Id), Times.Once);
    }
}
