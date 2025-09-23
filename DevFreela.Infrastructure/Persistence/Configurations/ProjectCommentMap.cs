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
    internal class ProjectCommentMap : IEntityTypeConfiguration<ProjectComment>
    {
        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            builder.ToTable(nameof(ProjectComment));

            builder.HasOne(x => x.Project)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.ProjectId);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
