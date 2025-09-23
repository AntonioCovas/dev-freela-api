using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities
{
    public class UserSkill : BaseEntity
    {
        public UserSkill(int userId, int skillId)
        {
            UserId = userId;
            SkillId = skillId;
        }

        public int UserId { get; private set; }
        public User User { get; private set; }
        public int SkillId { get; private set; }
        public Skill Skill { get; private set; }
    }
}
