using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var command = new CreateProjectCommand()
            {
                IdClient = 1,
                IdFreelancer = 2,
                Title = "Título Teste",
                Description = "Desc",
                TotalCost = 10000
            };
            var handler = new CreateProjectCommandHandler(projectRepositoryMock.Object);
            
            // Act
            var id = await handler.Handle(command, new CancellationToken());

            // Asserts
            Assert.True(id > 0);
            projectRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Project>()), Times.Once());

        }
    }
}
