using MediatR;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Domain.Entities;
using VehicleReservation.Dto;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommand : IRequest<Result<Vehicle>>
    {
        public Vehicle? Vehicle { get; set; }
    }
    public class AddVehicleCommandHanlder : IRequestHandler<AddVehicleCommand, Result<Vehicle>>
    {
        private readonly IVehicleRepositoryAsync _vehicleRepository;
        public AddVehicleCommandHanlder(IVehicleRepositoryAsync vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result<Vehicle>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            Result<Vehicle> result = new();
            try
            {
                if (request.Vehicle != null)
                {
                    Vehicle vehicle = new();

                    vehicle.Maker = request.Vehicle.Maker;
                    vehicle.Model = request.Vehicle.Model;
                    vehicle.UniqueId = await getNewId();

                    var response = await _vehicleRepository.AddAsync(vehicle);
                    result.Success = true;
                    result.StatusCode = System.Net.HttpStatusCode.OK;
                    result.Data = response;

                    return result;
                }
            }
            catch (Exception ex)
            {
                return ExceptionMethod(result, ex);
            }

            return ExceptionMethod(result, null, "Please provide a proper vehicle model.");
        }

        private static Result<Vehicle> ExceptionMethod(Result<Vehicle> result, Exception? ex = null, string customErrorMessage = "")
        {
            if (ex != null)
            {
                result.ErrorMessage = ex.Message;
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            else if (string.IsNullOrWhiteSpace(customErrorMessage))
            {
                result.ErrorMessage = customErrorMessage;
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            result.Success = false;

            return result;
        }

        private async Task<string> getNewId()
        {
            var latestId = await getLatestFreeId();
            var newId = Convert.ToInt32(latestId.Substring(1, latestId.Length)) + 1;

            return string.Format("C{newId}", newId);
        }

        private async Task<string> getLatestFreeId()
        {
            var latestId = await _vehicleRepository.GetLatestFreeId();
            return latestId;
        }
    }
}
