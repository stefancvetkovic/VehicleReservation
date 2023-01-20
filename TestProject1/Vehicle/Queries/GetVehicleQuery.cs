using FluentAssertions;
using System.Diagnostics;
using VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle;
using VehicleReservation.Application.CQRS.Vehicles.Queries;
using VehicleReservation.Domain.Entities;
using VehicleReservation.Dto;

namespace VehicleReservation.Test.Vehicle.Queries
{
    using static VehicleReservation.Test.Testing;

    [TestFixture]
    public class GetVehicleQuery
    {
        [Test]
        public async Task ShouldReturnAllVehicles()
        {
            //Arrange
            var query = new GetAllVehiclesQuery();

            //Act
            var result = await SendAsync(query);
            var data = result.Data;

            //Assert
            data.Should().NotBeEmpty().And.HaveCountGreaterThan(0);
        }

        [Test]
        public async Task ShouldAddVehicle()
        {
            //Arrange
            VehicleDto vehicle = new VehicleDto();
            vehicle.Maker = "BMW";
            vehicle.Model = "M4";
            
            var query = new AddVehicleCommand{ Vehicle = vehicle };

            //Act
            var result = await SendAsync(query);
            var data = result.Data;

            //Assert
            data.Should().BeOfType<VehicleDto>();
        }
    }
}
