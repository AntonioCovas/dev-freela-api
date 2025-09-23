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
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(254);

            builder.Property(x => x.FullName)
                   .IsRequired()
                   .HasMaxLength(254);

            builder.HasMany(x => x.UserSkills)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.OwnedProjects)
                   .WithOne(x => x.Client)
                   .HasForeignKey(x => x.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.FreelanceProjects)
                   .WithOne(x => x.Freelancer)
                   .HasForeignKey(x => x.FreelancerId)
                   .OnDelete(DeleteBehavior.Restrict);  

            builder.HasMany(x => x.Comments)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
