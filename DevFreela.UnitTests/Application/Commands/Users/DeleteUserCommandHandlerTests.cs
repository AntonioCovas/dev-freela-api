using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Users;

public class DeleteUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new DeleteUserCommandHandler(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenExistingUser_WhenHandling_ThenShouldFetchUser_AndCallDeleteAsync()
    {
        // Arrange
        var user = new User("João Silva", "joao@email.com", new DateTime(1990, 1, 1), "senha", "client");
        var command = new DeleteUserCommand(1);

        _userRepositoryMock
            .Setup(r => r.GetUserByIdAsync(command.Id))
            .ReturnsAsync(user);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.GetUserByIdAsync(command.Id), Times.Once);
        _userRepositoryMock.Verify(r => r.DeleteAsync(user), Times.Once);
    }
}
