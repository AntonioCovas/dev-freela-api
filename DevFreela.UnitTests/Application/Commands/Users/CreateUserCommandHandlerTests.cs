using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Users;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _authServiceMock = new Mock<IAuthService>();
        _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _authServiceMock.Object);
    }

    [Fact]
    public async Task GivenValidCommand_WhenHandling_ThenShouldHashPassword_AddUser_AndReturnNewId()
    {
        // Arrange
        var command = new CreateUserCommand("João Silva", "joao@email.com", new DateTime(1990, 1, 15), "client", "senha123");
        const string hashedPassword = "hash_da_senha";
        const int newUserId = 1;

        _authServiceMock
            .Setup(a => a.HashPassword(command.Password))
            .Returns(hashedPassword);

        _userRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(newUserId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(newUserId, result);
        _authServiceMock.Verify(a => a.HashPassword(It.IsAny<string>()), Times.Once);
        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
    }
}
