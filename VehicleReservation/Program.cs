using Microsoft.OpenApi.Models;
using Serilog;
using VehicleReservation.Application;
using VehicleReservation.Persistance;
using VehicleReservation.WebApi;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=> 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleReservation", Version = "v1" });
});

Serilog.Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddApplicationLayer();
builder.Services.AddPersistanceLayer(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseErrorHandlingMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
