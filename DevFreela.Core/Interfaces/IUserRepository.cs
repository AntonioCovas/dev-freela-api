using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<int> AddAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
        Task<User> GetUserByEmailAsync(string email, string password);
    }
}
