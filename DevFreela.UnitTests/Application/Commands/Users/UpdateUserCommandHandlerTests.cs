using DevFreela.Application.Commands.UpdateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.Users;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new UpdateUserCommandHandler(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenExistingUser_WhenHandling_ThenShouldUpdateUserData_AndSaveChanges()
    {
        // Arrange
        var user = new User("Nome Antigo", "antigo@email.com", new DateTime(1990, 1, 1), "senha", "client");
        var command = new UpdateUserCommand
        {
            Id = 1,
            FullName = "Nome Atualizado",
            Email = "novo@email.com",
            BirthDate = new DateTime(1992, 5, 20),
            Active = true
        };

        _userRepositoryMock
            .Setup(r => r.GetUserByIdAsync(command.Id))
            .ReturnsAsync(user);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal("Nome Atualizado", user.FullName);
        Assert.Equal("novo@email.com", user.Email);
        _userRepositoryMock.Verify(r => r.GetUserByIdAsync(command.Id), Times.Once);
        _userRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
