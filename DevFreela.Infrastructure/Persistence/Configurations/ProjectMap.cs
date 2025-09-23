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
    internal class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project));

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(254);

            builder.Property(x => x.Description)
                   .IsRequired()
                   .HasMaxLength(254);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.TotalCost)
                   .IsRequired()
                   .HasColumnType("DECIMAL(18, 2)");

            builder.HasOne(x => x.Freelancer)
                   .WithMany(x => x.FreelanceProjects)
                   .HasForeignKey(x => x.FreelancerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Client)
                   .WithMany(x => x.OwnedProjects)
                   .HasForeignKey(x => x.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
