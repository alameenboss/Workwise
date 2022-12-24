using Microsoft.EntityFrameworkCore;
using Workwise.API.Data.EFCore;
using Workwise.API.Service.Extentions;

using Workwise.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServiceLayer();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddServiceLayer();

var Configuration = builder.Configuration;

builder.Services.AddDbContext<WorkwiseDbContext>(x =>
    x.UseSqlServer(Configuration.GetConnectionString("WorkwiseDb")));


var app = builder.Build();

//var logger = app.Services.GetRequiredService<ILogger>();
//builder.Services.AddSingleton(typeof(ILogger), logger);


//app.ConfigureExceptionHandler(logger);
//app.ConfigureCustomExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
