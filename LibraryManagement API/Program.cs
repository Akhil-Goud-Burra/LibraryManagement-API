using LibraryManagement_API.Custom_Error_Responses;
using LibraryManagement_API.Error_Handling.Custom_Exception_Setup;
using LibraryManagement_API.Global_Exception_Middleware.Custom_Middleware;
using LibraryManagement_API.Models;
using LibraryManagement_API.RepositoryPattern.IRepository;
using LibraryManagement_API.RepositoryPattern.IRepositoryImplementation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. configure a database context (MyDbContext) for dependency injection
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

// Register the Repository Pattern
builder.Services.AddScoped<IRepositoryStream, IRepositoryStreamImplementation>();


// Global Exception Handling : Adding services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registering the Filter Globally
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});





var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Global Exception
    app.UseDeveloperExceptionPage();
}
else
{

    // Global Exception Handling Middleware
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = errorFeature?.Error;

            var errorResponse = new ErrorResponse
            {
                Message = "An unexpected error occurred.",
                Detail = app.Environment.IsDevelopment() ? exception?.Message : null, // Show details only in Development
                TraceId = context.TraceIdentifier // Useful for correlating logs
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        });
    });
}


//Register the Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
