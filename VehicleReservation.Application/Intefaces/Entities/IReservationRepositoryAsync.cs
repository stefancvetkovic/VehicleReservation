using VehicleReservation.Application.Interfaces;
using VehicleReservation.Domain.Entities;

namespace VehicleReservation.Application.Intefaces.Entities
{
    public interface IReservationRepositoryAsync : IGenericRepositoryAsync<Reservation>
    {
        bool HasFreeVehicleForPeriod(DateTime startFrom, DateTime EndTo, string vehicleId);
    }
}
