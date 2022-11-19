using Aula7.Api;
using Aula7.Api.Interfaces;
using Aula7.Api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//The default lifetime of the context is scoped,
//but the inMemory database(The data itself) will remain the lifetime of the application (aka singleton)
builder.Services.AddDbContext<EfContext>(optionsBuilder =>
{
    //optionsBuilder.UseSqlServer("connectionString");
    optionsBuilder.UseInMemoryDatabase("Aula7")
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();

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
