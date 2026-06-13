using DevFreela.Application.Commands.Project.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Projects;

public class CreateProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly CreateProjectCommandHandler _handler;

    public CreateProjectCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _handler = new CreateProjectCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenValidCommand_WhenHandling_ThenShouldCallAddAsync_AndReturnNewId()
    {
        // Arrange
        CreateProjectCommand command = new()
        {
            Title = "Projeto Teste",
            Description = "Descrição do projeto teste",
            IdClient = 1,
            IdFreelancer = 2,
            TotalCost = 5000m
        };

        _projectRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Project>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(1, result);
        _projectRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Project>()), Times.Once);
    }
}
