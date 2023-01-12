using Microsoft.OpenApi.Models;
using Serilog;
using System.Runtime.CompilerServices;
using VehicleReservation.Application;
using VehicleReservation.Persistance;
using VehicleReservation.Persistance.Context;
using VehicleReservation.WebApi;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        ConfigurationManager configuration = builder.Configuration;
        // Add services to the container.
        //builder.Services.AddScoped<IDbInitializer, DbInitializer>();
        builder.Services.AddControllers();
                       

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleReservation", Version = "v1" });
        });

        Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
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

        SeedDatabase(app);


        app.Run();


    }

    private static void SeedDatabase(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var vehicleReservationContext = services.GetRequiredService<VehicleReservationContext>();
            VehicleReservationContextSeed.SeedAsync(vehicleReservationContext);
        }
    }
}