using VehicleReservation.Domain.Entities;

namespace VehicleReservation.Persistance.Context
{
    public class VehicleReservationContextSeed
    {
        public static void SeedAsync(VehicleReservationContext vehicleReservationContext) 
        {
            if (!vehicleReservationContext.Vehicles.Any())
            {
                var vehicles = new List<Vehicle>
                {
                    new Vehicle { UniqueId = "C1", Maker ="BMW", Model = "X5"},
                    new Vehicle { UniqueId = "C2", Maker ="Skoda", Model = "Superb"},
                    new Vehicle { UniqueId = "C3", Maker ="Ford", Model = "Kuga"},
                    new Vehicle { UniqueId = "C4", Maker ="BMW", Model = "X3"},
                    new Vehicle { UniqueId = "C5", Maker ="Mazda", Model = "CX-5"},
                    new Vehicle { UniqueId = "C6", Maker ="Mercedes", Model = "E550"}
                };

                vehicleReservationContext.Vehicles.AddRange(vehicles);
                vehicleReservationContext.SaveChanges();
            }
        }
    }
}
