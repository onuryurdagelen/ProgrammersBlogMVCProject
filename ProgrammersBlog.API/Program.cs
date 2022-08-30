using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.DataAccess.EntityFramework.Contexts;
using ProgrammersBlog.Shared.DependencyResolvers;
using ProgrammersBlog.Shared.IoC;
using ProgrammersBlog.Shared.Utilities.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

//builder.Services.AddDependencyResolvers(new ICoreModule[]
//{
//    new CoreModule()
//});

builder.Services.AddDbContext<ProgrammersBlogContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"], sqlOptions =>
    {
        {
            sqlOptions.MigrationsAssembly("ProgrammersBlog.DataAccess");
        }
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
