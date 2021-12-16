using Datalayer;
using ServiceLayer.Pressures;
using ServiceLayer.Tempratures;
using Microsoft.EntityFrameworkCore;
using Pomelo;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(settings => settings.UseMySql(configuration.GetValue<String>("ConnectionString"), MariaDbServerVersion.LatestSupportedServerVersion));

builder.Services.AddScoped<ITempratureService, TempratureService>();
builder.Services.AddScoped<IPressureService, PressureService>();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation(configuration.GetValue<String>("ConnectionString"));

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
