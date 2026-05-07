using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.Skills;

public class GetAllSkillsQueryHandlerTests
{
    private readonly Mock<ISkillRepository> _skillRepositoryMock;
    private readonly GetAllSkillsQueryHandler _handler;

    public GetAllSkillsQueryHandlerTests()
    {
        _skillRepositoryMock = new Mock<ISkillRepository>();
        _handler = new GetAllSkillsQueryHandler(_skillRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenExistingSkills_WhenHandling_ThenShouldReturnSkillViewModelList()
    {
        // Arrange
        var skills = new List<Skill>
        {
            new("C#"),
            new("React"),
            new("SQL")
        };
        var query = new GetAllSkillsQuery();

        _skillRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(skills);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(skills.Count, result.Count);
        Assert.Equal(skills[0].Description, result[0].Description);
        _skillRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GivenNoSkills_WhenHandling_ThenShouldReturnEmptyList()
    {
        // Arrange
        _skillRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Skill>());

        var query = new GetAllSkillsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
