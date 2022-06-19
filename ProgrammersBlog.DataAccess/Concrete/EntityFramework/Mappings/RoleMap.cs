using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.Name).HasMaxLength(30);
            builder.Property(r => r.Description).IsRequired();
            builder.Property(r => r.Description).HasMaxLength(250);
            builder.Property(r => r.CreateByName).IsRequired();
            builder.Property(r => r.CreateByName).HasMaxLength(50);
            builder.Property(r => r.ModifiedByName).IsRequired();
            builder.Property(r => r.ModifiedByName).HasMaxLength(50);
            builder.Property(r => r.CreatedDate).IsRequired();
            builder.Property(r => r.IsDeleted).IsRequired();
            builder.Property(r => r.IsActive).IsRequired();
            builder.Property(r => r.Note).HasMaxLength(500);

            builder.ToTable("Roles");

            builder.HasData(new Role
            {
                Id = 1,
                Name = "Adming",
                Description = "Admin rolu tum haklara sahiptir.",
                IsActive = true,
                IsDeleted = false,
                CreateByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialCreate",
                Note = "Admin Roludur"
            });

        }
    }
}
