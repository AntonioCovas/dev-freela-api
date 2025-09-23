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
    internal class SkillMap : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable(nameof(Skill))
                   .HasKey(x => x.Id);

            builder.Property(x => x.Description)
                   .IsRequired()
                   .HasMaxLength(254);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();
        }
    }
}
