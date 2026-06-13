using DevFreela.Application.Queries.Project.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.Projects;

public class GetAllProjectsQueryHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly GetAllProjectsQueryHandler _handler;

    public GetAllProjectsQueryHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _handler = new GetAllProjectsQueryHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenExistingProjects_WhenHandling_ThenShouldReturnProjectViewModelList()
    {
        // Arrange
        var projects = new List<Project>
        {
            new("Projeto A", "Descrição A", 1, 2, 1000m),
            new("Projeto B", "Descrição B", 3, 4, 2000m)
        };
        var query = new GetAllProjectsQuery("");

        _projectRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(projects);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projects.Count, result.Count);
        Assert.Equal(projects[0].Title, result[0].Title);
        Assert.Equal(projects[1].Title, result[1].Title);
        _projectRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GivenNoProjects_WhenHandling_ThenShouldReturnEmptyList()
    {
        // Arrange
        _projectRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Project>());

        var query = new GetAllProjectsQuery("");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
