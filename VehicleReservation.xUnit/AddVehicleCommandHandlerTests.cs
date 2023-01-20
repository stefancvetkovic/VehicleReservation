using AutoMapper;
using Moq;
using Shouldly;
using System.Linq.Expressions;
using VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Application.Interfaces;
using VehicleReservation.Application.Mappings;
using VehicleReservation.Domain.Entities;
using VehicleReservation.Dto;
using VehicleReservation.Test.Mocks;
using Xunit;

namespace VehicleReservation.Test
{
    public class AddVehicleCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IVehicleRepositoryAsync> _mockUow;
        private readonly VehicleDto _vehicleDto;
        private readonly AddVehicleCommandHanlder _handler;

        public AddVehicleCommandHandlerTests()
        {
            _mockUow = MockLeaveTypeRepository.GetVehicleRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<VehicleProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new AddVehicleCommandHanlder(_mockUow.Object, _mapper);

            new VehicleDto
            {
                UniqueId = "C6",
                Maker = "Mercedes",
                Model = "ML"
            };
        }

        [Fact]
        public async Task ShouldAddVehicle()
        {
            var result = await _handler.Handle(new AddVehicleCommand() { Vehicle = _vehicleDto }, CancellationToken.None);

            var leaveTypes = await _mockUow.Object.GetAllAsync();

            result.ShouldBeOfType<Result<VehicleDto>>();
            leaveTypes.Count.ShouldBeGreaterThan(0);
        }
    }
}
