using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Application.Interfaces;
using VehicleReservation.Persistance.Context;
using VehicleReservation.Persistance.Repository;
using VehicleReservation.Persistance.Repository.Entities;

namespace VehicleReservation.Persistance
{
    public static class ServiceExtension
    {
        public static void AddPersistanceLayer(this IServiceCollection services)
        {
            services.AddDbContextFactory<VehicleReservationContext>((sp, options) =>
            {
                options.UseInMemoryDatabase("VehicleReservationDb");
            })
            .AddTransient<VehicleReservationContext>(p => p.GetRequiredService<IDbContextFactory<VehicleReservationContext>>()
            .CreateDbContext());

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            //services.AddTransient<IInvoiceLogRepositoryAsync, InvoiceLogRepositoryAsync>();
            services.AddTransient<IVehicleRepositoryAsync, VehicleRepositoryAsync>();
            services.AddTransient<IReservationRepositoryAsync, ReservationRepositoryAsync>();
        }
    }
}
