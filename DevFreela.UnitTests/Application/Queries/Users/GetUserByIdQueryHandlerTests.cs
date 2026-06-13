using DevFreela.Application.Queries.User.GetUserById;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.Users;

public class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new GetUserByIdQueryHandler(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenExistingUser_WhenHandling_ThenShouldReturnUserViewModel()
    {
        // Arrange
        var user = new User("Maria Santos", "maria@email.com", new DateTime(1985, 3, 10), "senha", "freelancer");
        var query = new GetUserByIdQuery(1);

        _userRepositoryMock
            .Setup(r => r.GetUserByIdAsync(query.Id))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.FullName, result.Name);
        _userRepositoryMock.Verify(r => r.GetUserByIdAsync(query.Id), Times.Once);
    }
}
