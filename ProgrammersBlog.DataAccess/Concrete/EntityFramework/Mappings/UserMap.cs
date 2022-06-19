using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.FirstName).HasMaxLength(30);
            builder.Property(u => u.LastName).HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.EmailAddress).HasMaxLength(100);
            builder.HasIndex(u => u.EmailAddress).IsUnique();
            builder.Property(u => u.EmailAddress).IsRequired();
            builder.Property(u => u.UserName).HasMaxLength(20);
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(500)");
            builder.Property(u => u.Description).HasMaxLength(500);
            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(250);
            builder.Property(u => u.CreateByName).IsRequired();
            builder.Property(u => u.CreateByName).HasMaxLength(50);
            builder.Property(u => u.ModifiedByName).IsRequired();
            builder.Property(u => u.ModifiedByName).HasMaxLength(50);
            builder.Property(u => u.CreatedDate).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.Note).HasMaxLength(500);

            builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);

            builder.ToTable("Users");

            builder.HasData(new User
            {
                Id=1,
                RoleId = 1,
                IsActive = true,
                IsDeleted = false,
                FirstName = "Onur",
                LastName = "Yurdagelen",
                UserName = "onuryurdagelen",
                EmailAddress = "yurdagelenonur1@gmail.com",
                CreateByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedDate =DateTime.Now,
                Description = "Ilk admin kullanici",
                Note = "Admin Kullanicisi",
                PasswordHash = Encoding.ASCII.GetBytes("ab6ef0a3f5a07a702780418c3145b688"),
                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU"
            });

            
        }
    }
}
