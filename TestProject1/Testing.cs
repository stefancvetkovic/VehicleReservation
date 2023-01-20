using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Respawn;
using VehicleReservation.Persistance.Context;
using VehicleReservation.WebApi;
namespace VehicleReservation.Test
{
    [SetUpFixture]
    public static class Testing
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory? _scopeFactory;
        private static Checkpoint _checkpoint;

        [OneTimeSetUp]
        public static void RunBeforeAnyTests()
        {
            var builder = WebApplication.CreateBuilder();
            var services = new ServiceCollection();

            var startup = new Startup(_configuration);
            startup.ConfigureServices(services);
            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w => w.ApplicationName == "VehicleReservation.WebApi" && w.EnvironmentName == "Development"));
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
            //_checkpoint = new Checkpoint();
        }

        public static async Task ResetState()
        {
            //await _checkpoint.Reset(_configuration.GetConnectionString());
        }

        public static async Task<TEntity> FindAsync<TEntity>(string id)
        where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<VehicleReservationContext>();

            return await context.FindAsync<TEntity>(id);
        }
        public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<VehicleReservationContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }
        public static async Task UpdateAsync<TEntity>(TEntity entity)
       where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<VehicleReservationContext>();

            context.Update(entity);

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