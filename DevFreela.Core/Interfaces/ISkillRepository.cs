using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllAsync();
    }
}
