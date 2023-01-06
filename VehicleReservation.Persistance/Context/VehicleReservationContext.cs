using Microsoft.EntityFrameworkCore;
using VehicleReservation.Domain.Entities;

namespace VehicleReservation.Persistance.Context
{
    public class VehicleReservationContext : DbContext
    {
        public VehicleReservationContext()
        {
        }

        public VehicleReservationContext(DbContextOptions<VehicleReservationContext> options)
            :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseInMemoryDatabase("VehicleReservationDb");

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}