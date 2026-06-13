using DevFreela.Application.Commands.Project.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Projects;

public class UpdateProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly UpdateProjectCommandHandler _handler;

    public UpdateProjectCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenExistingProject_WhenHandling_ThenShouldUpdateProjectData_AndSaveChanges()
    {
        // Arrange
        Project project = new("Título Antigo", "Descrição Antiga", 1, 2, 3000m);
        UpdateProjectCommand updateProjectCommand = new()
        {
            Id = 1,
            Title = "Título Atualizado",
            Description = "Descrição Atualizada",
            TotalCost = 6000m
        };
        UpdateProjectCommand command = updateProjectCommand;

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id))
            .ReturnsAsync(project);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal("Título Atualizado", project.Title);
        Assert.Equal("Descrição Atualizada", project.Description);
        Assert.Equal(6000m, project.TotalCost);
        _projectRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
        _projectRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
