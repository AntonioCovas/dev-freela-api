using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void TestIfProjectStartWorks()
        {
            var project = new Project("Nome Teste", "Desc Teste", 1, 2, 10000);

            Assert.NotEmpty(project.Title);
            Assert.NotEmpty(project.Title);

            Assert.NotEmpty(project.Description);
            Assert.NotEmpty(project.Description);

            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.Equal(project.StartedAt, DateTime.MinValue);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotEqual(project.StartedAt, DateTime.MinValue);
        }
    }
}
