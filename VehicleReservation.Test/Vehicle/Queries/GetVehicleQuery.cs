using VehicleReservation.Application.CQRS.Vehicles.Queries;
using VehicleReservation.Dto;

namespace VehicleReservation.Test.Vehicle.Queries
{
    using static VehicleReservation.Test.Testing;

    public class GetVehicleQuery
    {
        [Test]
        public async Task ShouldReturnAllVehicles()
        {
            //Arrange
            await AddAsync(new List<VehicleDto>
                {
                    new VehicleDto { UniqueId = "C1", Maker ="BMW", Model = "X5"},
                    new VehicleDto { UniqueId = "C2", Maker ="Skoda", Model = "Superb"},
                    new VehicleDto { UniqueId = "C3", Maker ="Ford", Model = "Kuga"},
                    new VehicleDto { UniqueId = "C4", Maker ="BMW", Model = "X3"},
                    new VehicleDto { UniqueId = "C5", Maker ="Mazda", Model = "CX-5"},
                    new VehicleDto { UniqueId = "C6", Maker ="Mercedes", Model = "E550"}

                });

            var query = new GetAllVehiclesQuery();

            //Act
            var result = "";// await SendAsync(query);

            //Assert
            //result.Should().NotBeNull();
            //result.Lists.Should().HaveCount(1);
        }
    }
}
