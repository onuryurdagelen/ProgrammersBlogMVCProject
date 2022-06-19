using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(70);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.CreateByName).IsRequired();
            builder.Property(c => c.CreateByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);

            builder.ToTable("Categories");

            builder.HasData(
                new Category
            {
                Id = 1,
                Name = "C#",
                Description = "C# Programlama Dili İle ilgili En Güncel Bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreateByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialCreate",
                Note = "C# Blog Kategorisi"
            },
            new Category
            {
                Id = 2,
                Name = "C++",
                Description = "C++ Programlama Dili İle ilgili En Güncel Bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreateByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialCreate",
                Note = "C++ Blog Kategorisi"
            },
            new Category
            {
                Id = 3,
                Name = "Java",
                Description = "Java Programlama Dili İle ilgili En Güncel Bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreateByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialCreate",
                Note = "Java Blog Kategorisi"
            },
            new Category
            {
                Id = 4,
                Name = "JavaScript",
                Description = "JavaScript Programlama Dili İle ilgili En Güncel Bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreateByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialCreate",
                Note = "JavaScript Blog Kategorisi"
            }
            );
        }
    }
}
