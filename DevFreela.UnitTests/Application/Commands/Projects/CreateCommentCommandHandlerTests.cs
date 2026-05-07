using DevFreela.Application.Commands.CreateComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Projects;

public class CreateCommentCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly CreateCommentCommandHandler _handler;

    public CreateCommentCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _handler = new CreateCommentCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenValidCommand_WhenHandling_ThenShouldCallAddCommentAsync()
    {
        // Arrange
        CreateCommentCommand command = new()
        {
            Content = "Ótimo projeto!",
            ProjectId = 1,
            UserId = 2
        };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _projectRepositoryMock.Verify(r => r.AddCommentAsync(It.IsAny<ProjectComment>()), Times.Once);
    }
}
