using AutoMapper;
using Moq;
using Shouldly;
using VehicleReservation.Application.CQRS.Vehicles.Commands.DeleteVehicle;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Application.Mappings;
using VehicleReservation.Dto;
using VehicleReservation.Test.Mocks;

namespace VehicleReservation.xUnit
{
    public class DeleteVehicleCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IVehicleRepositoryAsync> _mockUow;
        private readonly VehicleDto _vehicleDto;
        private readonly DeleteVehicleCommandHanlder _handler;

        public DeleteVehicleCommandHandlerTest()
        {
            _mockUow = MockLeaveTypeRepository.GetVehicleRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<VehicleProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new DeleteVehicleCommandHanlder(_mockUow.Object, _mapper);


            new VehicleDto
            {
                UniqueId = "C1",
                Maker = "Mercedes",
                Model = "ML"
            };
        }

        [Fact]
        public async Task ShouldDeleteVehicle()
        {
            var result = await _handler.Handle(new DeleteVehicleCommand() { VehicleId = "C1" }, CancellationToken.None);
            var vehicleForDelete = await _mockUow.Object.GetById("C1");

            await _mockUow.Object.DeleteAsync(vehicleForDelete);

            result.ShouldBeOfType<Result<string>>();
        }
    }
}
