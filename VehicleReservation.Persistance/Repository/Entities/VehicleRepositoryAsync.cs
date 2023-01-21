using Microsoft.EntityFrameworkCore;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Domain.Entities;
using VehicleReservation.Persistance.Context;

namespace VehicleReservation.Persistance.Repository.Entities
{
    public class VehicleRepositoryAsync : GenericRepositoryAsync<Vehicle>, IVehicleRepositoryAsync
    {
        private readonly DbSet<Vehicle> _vehicles;

        public VehicleRepositoryAsync(VehicleReservationContext dbContext) : base(dbContext)
        {
            _vehicles = dbContext.Set<Vehicle>();
        }

        public async Task<string> GetLatestFreeId()
        {
            return (await _vehicles.OrderByDescending(x => x.UniqueId).FirstOrDefaultAsync())?.UniqueId!;
        }

        public async Task<Vehicle> GetByIdAsync(string id)
        {
            return await _vehicles.FirstOrDefaultAsync(x => x.UniqueId == id);
        }

        public bool HasFreeVehicleId(string id)
        {
            return !_vehicles.Any(x => x.UniqueId == id);
        }
    }
}
