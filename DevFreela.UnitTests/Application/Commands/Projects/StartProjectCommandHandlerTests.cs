using DevFreela.Application.Commands.Project.StartProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Projects;

public class StartProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly StartProjectCommandHandler _handler;

    public StartProjectCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _handler = new StartProjectCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenCreatedProject_WhenHandling_ThenShouldStartProject_AndCallStartAsync()
    {
        // Arrange
        Project project = new("Projeto", "Descrição", 1, 2, 5000m);
        StartProjectCommand command = new(1);

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id))
            .ReturnsAsync(project);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotEqual(DateTime.MinValue, project.StartedAt);
        _projectRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
        _projectRepositoryMock.Verify(r => r.StartAsync(project), Times.Once);
    }
}
