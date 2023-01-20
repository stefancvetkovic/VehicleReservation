using Moq;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Domain.Entities;

namespace VehicleReservation.Test.Mocks
{
    public static class MockLeaveTypeRepository
    {
        public static Mock<IVehicleRepositoryAsync> GetVehicleRepository()
        {
            var leaveTypes = new List<Vehicle>
            {
                new Vehicle
                {
                    UniqueId = "C1",
                    Maker = "BMW",
                    Model = "M2"
                },
                new Vehicle
                {
                    UniqueId = "C2",
                    Maker = "Honda",
                    Model = "Accord"
                },
                 new Vehicle
                {
                    UniqueId = "C3",
                    Maker = "Skoda",
                    Model = "Superb"
                }
            };

            var mockRepo = new Mock<IVehicleRepositoryAsync>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Vehicle>())).ReturnsAsync((Vehicle vehicle) => 
            {
                leaveTypes.Add(vehicle);
                return vehicle;
            });

            return mockRepo;
        }
    }
}
