using AutoMapper;
using Moq;
using Shouldly;
using VehicleReservation.Application.CQRS.Vehicles.Commands.UpdateVehicle;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Application.Mappings;
using VehicleReservation.Dto;
using VehicleReservation.Test.Mocks;

namespace VehicleReservation.xUnit
{
    public class UpdateVehicleCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IVehicleRepositoryAsync> _mockUow;
        private readonly VehicleDto _vehicleDto;
        private readonly UpdateVehicleCommandHanlder _handler;

        public UpdateVehicleCommandHandlerTest()
        {
            _mockUow = MockLeaveTypeRepository.GetVehicleRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<VehicleProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateVehicleCommandHanlder(_mockUow.Object, _mapper);

            new VehicleDto
            {
                UniqueId = "C1",
                Maker = "Mercedes",
                Model = "ML"
            };
        }

        [Fact]
        public async Task ShouldUpdateVehicle()
        {
            var result = await _handler.Handle(new UpdateVehicleCommand() { Vehicle = _vehicleDto }, CancellationToken.None);
            var vehicles = await _mockUow.Object.GetAllAsync();

            result.ShouldBeOfType<Result<VehicleDto>>();
            vehicles.Count.ShouldBeGreaterThan(0);
        }
    }
}
