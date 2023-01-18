using Microsoft.OpenApi.Models;
using Serilog;
using VehicleReservation.Application;
using VehicleReservation.Persistance;
using VehicleReservation.Persistance.Context;

namespace VehicleReservation.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddPersistanceLayer(Configuration);
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleReservation", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseErrorHandlingMiddleware();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedDatabase(app);
        }
        private void SeedDatabase(IApplicationBuilder app)
        {
            
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var vehicleReservationContext = services.GetRequiredService<VehicleReservationContext>();
                VehicleReservationContextSeed.SeedAsync(vehicleReservationContext);
            }
        }
    }
}
