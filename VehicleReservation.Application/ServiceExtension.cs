using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VehicleReservation.Application.Behaviors;
using VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle;
using VehicleReservation.Dto;

namespace VehicleReservation.Application
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
