using AutoMapper;
using Moq;
using Shouldly;
using VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Dto;
using Xunit;

namespace VehicleReservation.Test
{
    public class AddVehicleCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IVehicleRepositoryAsync> _mockUow;
        private readonly VehicleDto _vehicleDto;
        private readonly AddVehicleCommandHanlder _handler;

        public AddVehicleCommandHandlerTests(IMapper mapper, Mock<IVehicleRepositoryAsync> mockUow, VehicleDto vehicleDto, AddVehicleCommandHanlder handler)
        {
            _mapper = mapper;
            _mockUow = mockUow;
            _vehicleDto = vehicleDto;
            _handler = handler;
        }

        [Fact]
        public async Task Valid_LeaveType_Added()
        {
            var result = await _handler.Handle(new AddVehicleCommand() { Vehicle = _vehicleDto }, CancellationToken.None);

            var leaveTypes = await _mockUow.Object.GetAllAsync();

            result.ShouldBeOfType<Result<VehicleDto>>();

            leaveTypes.Count.ShouldBe(4);
        }
    }
}
