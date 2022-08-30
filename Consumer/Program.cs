using Consumer.Client;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<HttpContextMiddleware>();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
// Add services to the container.
builder.Services.AddApiClient(client =>
{
    client.BaseAddress = new Uri("https://localhost:5045");
});
builder.Services.AddApiClient(client =>
{
    client.BaseAddress = new Uri("https://localhost:5060");
});

//ApiClientRegister Class'ýný implemente eder.
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
