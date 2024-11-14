using LibraryManagement_API.IRepository;
using LibraryManagement_API.IRepository.RepositoryImplementation;
using LibraryManagement_API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. configure a database context (MyDbContext) for dependency injection
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

// Register the generic repository with a transient or scoped lifetime
builder.Services.AddScoped(typeof(IRepositoryLibraryManagement<>), typeof(RepositoryImplementationLibraryManagement<>));

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
