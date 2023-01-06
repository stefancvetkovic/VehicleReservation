using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleReservation.Application.Interfaces;
using VehicleReservation.Domain.Entities;

namespace VehicleReservation.Application.Intefaces.Entities
{
    public interface IVehicleRepositoryAsync : IGenericRepositoryAsync<Vehicle>
    {
        public Task<List<Vehicle>> GetVehiclesByManufacturer(string manufacturerShortCode);
    }
}
