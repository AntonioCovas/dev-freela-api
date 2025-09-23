using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    internal class UserSkillMap : IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {
            builder.ToTable(nameof(UserSkill))
                   .HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserSkills)
                   .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Skill)
                   .WithMany(x => x.UserSkills)
                   .HasForeignKey(x => x.SkillId);
        }
    }
}
