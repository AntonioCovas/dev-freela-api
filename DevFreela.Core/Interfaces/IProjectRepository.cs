using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
        Task<int> AddAsync(Project project);
        Task DeleteAsync(Project project);
        Task SaveChangesAsync();
        Task AddCommentAsync(ProjectComment projectComment);
        Task FinishAsync(int id);
        Task StartAsync(Project project);
    }
}
