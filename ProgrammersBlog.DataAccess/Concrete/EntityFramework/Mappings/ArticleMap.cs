using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a=> a.Title).HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired(true);
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a => a.SeoAuthor).IsRequired();
            builder.Property(a => a.SeoAuthor).HasMaxLength(50);
            builder.Property(a => a.SeoDescription).HasMaxLength(150);
            builder.Property(a => a.SeoDescription).IsRequired();
            builder.Property(a => a.SeoTags).IsRequired();
            builder.Property(a => a.SeoTags).HasMaxLength(70);
            builder.Property(a => a.ViewsCount).IsRequired();
            builder.Property(a => a.CommentCount).IsRequired();
            builder.Property(a => a.Thumbnail).IsRequired();
            builder.Property(a => a.Thumbnail).HasMaxLength(250);
            builder.Property(a => a.CreateByName).IsRequired();
            builder.Property(a => a.CreateByName).HasMaxLength(50);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50);
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500);

            //1-to-N relationship(Category - Article)
            builder.HasOne<Category>(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId);

            //1-to-N relationship(User - Article)

            builder.HasOne<User>(a => a.User)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.UserId);

            //creating table name for Article class
            builder.ToTable("Articles");

            //builder.HasData(
            //    new Article
            //    {
            //        Id=1,
            //        CategoryId =1,
            //        Title = "C# 9.0 ve .NET 5 Yenilikleri",
            //        Content = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
            //        Thumbnail = "Default.jpg",
            //        SeoDescription = "C# 9.0 ve .NET 5 Yenilikleri",
            //        SeoTags = "C#,C# 9,.NET 5,.NET Framework,.NET Core",
            //        SeoAuthor = "Onur Yurdagelen",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreateByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        Note = "Java Blog Kategorisi",
            //        UserId =1,
            //        ViewsCount = 40,
            //        CommentCount = 3
            //    },
            //    new Article
            //    {
            //        Id = 2,
            //        CategoryId = 2,
            //        Title = "C++ 11.0 ve 19 Yenilikleri",
            //        Content = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
            //        Thumbnail = "Default.jpg",
            //        SeoDescription = "C++ 11 ve 19 Yenilikleri",
            //        SeoTags = "C++,C++ 11,C++ 19",
            //        SeoAuthor = "Onur Yurdagelen",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreateByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        Note = "C++ Blog Kategorisi",
            //        ViewsCount = 60,
            //        CommentCount = 6,
            //        UserId = 1
            //    },
            //    new Article
            //    {
            //        Id = 3,
            //        CategoryId = 3,
            //        Title = "Java 9,Java 11 Yeniliklero",
            //        Content = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
            //        Thumbnail = "Default.jpg",
            //        SeoDescription = "Javascript 9 ve 11 Yenilikleri",
            //        SeoTags = "Java,Java 9,Java 11",
            //        SeoAuthor = "Onur Yurdagelen",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreateByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        Note = "Java Blog Kategorisi",
            //        CommentCount=3,
            //        ViewsCount=12,
            //        UserId = 1
            //    }
            //    );
        }
    }
}
