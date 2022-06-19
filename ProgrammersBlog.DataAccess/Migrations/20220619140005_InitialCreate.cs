using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammersBlog.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateByName = table.Column<string>(maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateByName = table.Column<string>(maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateByName = table.Column<string>(maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(500)", nullable: false),
                    UserName = table.Column<string>(maxLength: 20, nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    Picture = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateByName = table.Column<string>(maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ViewsCount = table.Column<int>(nullable: false),
                    CommentCount = table.Column<int>(nullable: false),
                    SeoAuthor = table.Column<string>(maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(maxLength: 70, nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateByName = table.Column<string>(maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    Text = table.Column<string>(maxLength: 1000, nullable: false),
                    ArticleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 250, DateTimeKind.Local).AddTicks(2667), "C# Programlama Dili İle ilgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 250, DateTimeKind.Local).AddTicks(1459), "C#", "C# Blog Kategorisi" },
                    { 2, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 250, DateTimeKind.Local).AddTicks(2807), "C++ Programlama Dili İle ilgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 250, DateTimeKind.Local).AddTicks(2771), "C++", "C++ Blog Kategorisi" },
                    { 3, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 250, DateTimeKind.Local).AddTicks(2813), "Java Programlama Dili İle ilgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 250, DateTimeKind.Local).AddTicks(2810), "Java", "Java Blog Kategorisi" },
                    { 4, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 250, DateTimeKind.Local).AddTicks(2818), "JavaScript Programlama Dili İle ilgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 250, DateTimeKind.Local).AddTicks(2816), "JavaScript", "JavaScript Blog Kategorisi" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[] { 1, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 256, DateTimeKind.Local).AddTicks(4182), "Admin rolu tum haklara sahiptir.", true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 256, DateTimeKind.Local).AddTicks(3502), "Adming", "Admin Roludur" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateByName", "CreatedDate", "Description", "EmailAddress", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedByName", "ModifiedDate", "Note", "PasswordHash", "Picture", "RoleId", "UserName" },
                values: new object[] { 1, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 270, DateTimeKind.Local).AddTicks(2813), "Ilk admin kullanici", "yurdagelenonur1@gmail.com", "Onur", true, false, "Yurdagelen", "Admin", new DateTime(2022, 6, 19, 17, 0, 5, 270, DateTimeKind.Local).AddTicks(2820), "Admin Kullanicisi", new byte[] { 97, 98, 54, 101, 102, 48, 97, 51, 102, 53, 97, 48, 55, 97, 55, 48, 50, 55, 56, 48, 52, 49, 56, 99, 51, 49, 52, 53, 98, 54, 56, 56 }, "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU", 1, "onuryurdagelen" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreateByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 1, 1, 3, "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.", "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 237, DateTimeKind.Local).AddTicks(7323), new DateTime(2022, 6, 19, 17, 0, 5, 237, DateTimeKind.Local).AddTicks(5320), true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 237, DateTimeKind.Local).AddTicks(665), "Java Blog Kategorisi", "Onur Yurdagelen", "C# 9.0 ve .NET 5 Yenilikleri", "C#,C# 9,.NET 5,.NET Framework,.NET Core", "Default.jpg", "C# 9.0 ve .NET 5 Yenilikleri", 1, 40 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreateByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 2, 2, 6, "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.", "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 238, DateTimeKind.Local).AddTicks(370), new DateTime(2022, 6, 19, 17, 0, 5, 238, DateTimeKind.Local).AddTicks(350), true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 238, DateTimeKind.Local).AddTicks(217), "C++ Blog Kategorisi", "Onur Yurdagelen", "C++ 11 ve 19 Yenilikleri", "C++,C++ 11,C++ 19", "Default.jpg", "C++ 11.0 ve 19 Yenilikleri", 1, 60 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreateByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 3, 3, 3, "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.", "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 238, DateTimeKind.Local).AddTicks(439), new DateTime(2022, 6, 19, 17, 0, 5, 238, DateTimeKind.Local).AddTicks(435), true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 238, DateTimeKind.Local).AddTicks(429), "Java Blog Kategorisi", "Onur Yurdagelen", "Javascript 9 ve 11 Yenilikleri", "Java,Java 9,Java 11", "Default.jpg", "Java 9,Java 11 Yeniliklero", 1, 12 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreateByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 1, 1, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 254, DateTimeKind.Local).AddTicks(1225), true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 254, DateTimeKind.Local).AddTicks(512), "C# Blog Kategorisi", "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır." });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreateByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 2, 2, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 254, DateTimeKind.Local).AddTicks(1277), true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 254, DateTimeKind.Local).AddTicks(1259), "C++ Blog Kategorisi", "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır." });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreateByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 3, 3, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 254, DateTimeKind.Local).AddTicks(1283), true, false, "InitialCreate", new DateTime(2022, 6, 19, 17, 0, 5, 254, DateTimeKind.Local).AddTicks(1280), "Java Blog Kategorisi", "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır." });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailAddress",
                table: "Users",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
