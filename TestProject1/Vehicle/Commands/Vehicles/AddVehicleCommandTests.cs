using FluentAssertions;
using FluentValidation;
using VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle;
using VehicleReservation.Dto;
using static VehicleReservation.Test.Testing;
namespace VehicleReservation.Vehicle.Commands.Vehicles
{
    public class AddVehicleCommandTests
    {
        [Test]
        public void ShouldRequireMinimumFields() 
        {
            var command = new AddVehicleCommand();

            FluentActions.Invoking(() => SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueId()
        {
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
                    UniqueId = "C8"
                }
            };

            await FluentActions.Awaiting(() => SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateVehicle()
        {
            var command = new AddVehicleCommand
            {
                Vehicle = new VehicleDto
                {
                    Maker = "BMW",
                    Model = "M4",
                    UniqueId = "C10"
                }
            };

            var vehicle = await SendAsync(command);
            var id = vehicle.Data.UniqueId;

            var vehicleNew = await FindAsync<VehicleReservation.Domain.Entities.Vehicle>(id);

            vehicleNew.Should().NotBeNull();
        }
    }
}
