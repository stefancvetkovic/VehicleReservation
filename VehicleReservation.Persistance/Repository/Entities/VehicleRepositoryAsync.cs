using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Task<List<Vehicle>> GetVehiclesByManufacturer(string manufacturerShortCode)
        {
            throw new NotImplementedException();
        }
    }
}
