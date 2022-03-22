using Datalayer;
using ServiceLayer.Pressures;
using ServiceLayer.Tempratures;
using Microsoft.EntityFrameworkCore;
using Pomelo;
using ServiceLayer.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(settings => settings.UseMySql(configuration.GetValue<String>("ConnectionString"), MariaDbServerVersion.LatestSupportedServerVersion));

builder.Services.AddHostedService<AmqpService>();

builder.Services.AddTransient<ITempratureService, TempratureService>();
builder.Services.AddTransient<IPressureService, PressureService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.EnsureCreated();
}

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
