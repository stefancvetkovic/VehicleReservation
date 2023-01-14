using Microsoft.EntityFrameworkCore;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Domain.Entities;
using VehicleReservation.Persistance.Context;

namespace VehicleReservation.Persistance.Repository.Entities
{
    public class ReservationRepositoryAsync : GenericRepositoryAsync<Reservation>, IReservationRepositoryAsync
    {
        private readonly DbSet<Reservation> _reservations;

        public ReservationRepositoryAsync(VehicleReservationContext dbContext) : base(dbContext)
        {
            _reservations = dbContext.Set<Reservation>();
        }

        public bool HasFreeVehicleForPeriod(DateTime startFrom, DateTime endTo, string vehicleId)
        {
            var reservations = _reservations.Where(x => x.VehicleId == vehicleId).ToList();

            //https://www.codeproject.com/Articles/168662/Time-Period-Library-for-NET
            //We can use this library for time overlapping

            bool hasOverlap = false;
            
            foreach (var reservation in reservations)
            {
                hasOverlap = startFrom < reservation.EndTo && reservation.StartFrom < endTo;
                if (hasOverlap)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
