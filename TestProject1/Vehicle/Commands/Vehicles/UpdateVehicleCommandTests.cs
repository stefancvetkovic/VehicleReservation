using FluentAssertions;
using FluentValidation;
using VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle;
using VehicleReservation.Application.CQRS.Vehicles.Commands.UpdateVehicle;
using VehicleReservation.Dto;

namespace VehicleReservation.Test.Vehicle.Commands.Vehicles
{
    using static VehicleReservation.Test.Testing;

    public class UpdateVehicleCommandTests
    {
        [Test]
        public void ShouldRequireValidVehicle()
        {
            var command = new UpdateVehicleCommand 
            {
                Vehicle = new VehicleDto 
                {
                    Maker = "BMW",
                    Model = "Z4",
                    UniqueId = "C1"
                }
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueId()
        {
            var vehicle = await SendAsync(new AddVehicleCommand
            {
                Vehicle = new VehicleDto
                {
                    Maker = "BMW",
                    Model = "M4",
                    UniqueId = "C8"
                }
            });

            await SendAsync(new AddVehicleCommand
            {
                Vehicle = new VehicleDto
                {
                    Maker = "BMW",
                    Model = "M4",
                    UniqueId = "C8"
                }
            });

            var command = new AddVehicleCommand
            {
                Vehicle = new VehicleDto
                {
                    Maker = "BMW",
                    Model = "M4",
                    UniqueId = vehicle.Data.UniqueId
                }
            };

            var exception = FluentActions.Invoking(() => SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}
