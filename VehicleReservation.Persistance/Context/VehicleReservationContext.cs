using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Principal;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Maker)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.StartFrom)
                    .IsRequired();

                entity.Property(e => e.EndTo)
                    .IsRequired();

                entity.HasOne(d => d.Vehicle)
                  .WithMany(p => p.Reservation)
                  .HasForeignKey(d => d.VehicleId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseInMemoryDatabase("VehicleReservationDb");

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}