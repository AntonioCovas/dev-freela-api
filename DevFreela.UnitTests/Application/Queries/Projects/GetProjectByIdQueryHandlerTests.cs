using DevFreela.Application.Queries.Project.GetProjectById;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.Projects;

public class GetProjectByIdQueryHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly GetProjectByIdQueryHandler _handler;

    public GetProjectByIdQueryHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _handler = new GetProjectByIdQueryHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenExistingProject_WhenHandling_ThenShouldReturnProjectViewModel()
    {
        // Arrange
        var project = new Project("Projeto Teste", "Descrição", 1, 2, 3000m);
        var query = new GetProjectByIdQuery(1);

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(query.Id))
            .ReturnsAsync(project);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(project.Title, result.Title);
        _projectRepositoryMock.Verify(r => r.GetByIdAsync(query.Id), Times.Once);
    }
}
