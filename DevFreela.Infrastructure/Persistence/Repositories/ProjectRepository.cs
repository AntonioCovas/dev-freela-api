using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs")??string.Empty;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            var project = new Project(string.Empty, string.Empty, 0, 0, 0m);
            var projectAux = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == id);
            project = projectAux != null ? projectAux : project;
            return project;
        }

        public async Task<int> AddAsync(Project project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();
            return project.Id;
        }

        public async Task AddCommentAsync(ProjectComment projectComment)
        {
            _dbContext.ProjectComments.Add(projectComment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task FinishAsync(int id)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == id);
            project?.Finish();
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {            
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Project SET status = @status, startedAt = @startedAt WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new
                {
                    status = project.Status,
                    startedAt = project.StartedAt,
                    id = project.Id,
                });
            }
        }

        public async Task SaveChangesAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Project project)
        {
            _dbContext.Projects?.Remove(project);
            await _dbContext.SaveChangesAsync();
        }
    }
}
