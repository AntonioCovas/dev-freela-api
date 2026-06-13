using DevFreela.Application.Queries.Project.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            var projects = new List<Project>()
            {
                new("Nome Teste 1", "Desc Teste 1", 1, 2 , 10000),
                new("Nome Teste 2", "Desc Teste 2", 1, 2 , 20000),
                new("Nome Teste 3", "Desc Teste 3", 1,  2 , 30000)
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery(string.Empty);
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // Act
            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            // Assert (xUnit)
            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count);

            // Verifica se o método foi chamado uma vez (Moq)
            projectRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once());
        }
    }
}
