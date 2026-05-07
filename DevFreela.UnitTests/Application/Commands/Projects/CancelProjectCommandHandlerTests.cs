using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Projects;

public class CancelProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly CancelProjectCommandHandler _handler;

    public CancelProjectCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _handler = new CancelProjectCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenInProgressProject_WhenHandling_ThenShouldCancelProject_AndSaveChanges()
    {
        // Arrange
        var project = new Project("Projeto", "Descrição", 1, 2, 5000m);
        project.Start();
        var command = new CancelProjectCommand(1);

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id))
            .ReturnsAsync(project);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ProjectStatusEnum.Canceled, project.Status);
        _projectRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
        _projectRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
