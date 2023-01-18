using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using VehicleReservation.Persistance.Context;
using VehicleReservation.WebApi;

namespace VehicleReservation.Test
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfiguration app;
        private static IServiceScopeFactory? _scopeFactory;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = WebApplication.CreateBuilder();
            var services = new ServiceCollection();

            //var startup = new Startup(builder);

            //startup.ConfigureServices(services);
            //services.AddSingleton(Mock.Of<IWebHostEnvironment>(w => w.ApplicationName == "VehicleReservation.WebApi" && w.EnvironmentName == "Development"));

            //_scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<VehicleReservationContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }
    }
}