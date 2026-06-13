using DevFreela.Application.Commands.User.UserLogin;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using MediatR;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Users;

public class UserLoginCommandHandlerTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly IRequestHandler<UserLoginCommand, UserLoginViewModel> _handler;

    public UserLoginCommandHandlerTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new UserLoginCommandHandler(_authServiceMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenValidCredentials_WhenHandling_ThenShouldReturnLoginViewModel()
    {
        // Arrange
        var command = new UserLoginCommand { Email = "joao@email.com", Password = "senha123" };
        var user = new User("João Silva", "joao@email.com", new DateTime(1990, 1, 1), "hash_senha", "client");
        const string jwtToken = "jwt_token_gerado";

        _userRepositoryMock
            .Setup(r => r.GetUserByEmailAsync(command.Email, command.Password))
            .ReturnsAsync(user);

        _authServiceMock
            .Setup(a => a.VerifyPassword(user.Password, command.Password))
            .Returns(true);

        _authServiceMock
            .Setup(a => a.GenerateJwtToken(user.Email, user.Role))
            .Returns(jwtToken);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(jwtToken, result.Token);
    }

    [Fact]
    public async Task GivenNonExistentUser_WhenHandling_ThenShouldReturnNull()
    {
        // Arrange
        var command = new UserLoginCommand { Email = "inexistente@email.com", Password = "senha123" };

        _userRepositoryMock
            .Setup(r => r.GetUserByEmailAsync(command.Email, command.Password))
            .ReturnsAsync((User)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _authServiceMock.Verify(a => a.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task GivenInvalidPassword_WhenHandling_ThenShouldReturnNull()
    {
        // Arrange
        var command = new UserLoginCommand { Email = "joao@email.com", Password = "senha_errada" };
        var user = new User("João Silva", "joao@email.com", new DateTime(1990, 1, 1), "hash_senha", "client");

        _userRepositoryMock
            .Setup(r => r.GetUserByEmailAsync(command.Email, command.Password))
            .ReturnsAsync(user);

        _authServiceMock
            .Setup(a => a.VerifyPassword(user.Password, command.Password))
            .Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _authServiceMock.Verify(a => a.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}
