using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext devFreelaDbContext)
        {
            _dbContext = devFreelaDbContext;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = new User(string.Empty, string.Empty, new DateTime());
            var userAux = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            return userAux ?? user;
        }

        public async Task<int> AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            try { await _dbContext.SaveChangesAsync(); } catch (Exception ex) { }
            return user.Id;
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
