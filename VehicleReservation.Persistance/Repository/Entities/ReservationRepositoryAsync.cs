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
    }
}
