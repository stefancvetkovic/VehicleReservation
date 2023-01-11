using MediatR;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Dto;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<Result<string>>
    {
        public string? VehicleId { get; set; }
    }
    public class DeleteVehicleCommandHanlder<T> : IRequestHandler<DeleteVehicleCommand, Result<string>>
    {
        private readonly IVehicleRepositoryAsync _vehicleRepository;
        public DeleteVehicleCommandHanlder(IVehicleRepositoryAsync vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result<string>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            Result<string> result = new();
            try
            {
                if (string.IsNullOrWhiteSpace(request.VehicleId))
                {
                    var vehicle = await _vehicleRepository.GetById(request.VehicleId!);
                    await _vehicleRepository.DeleteAsync(vehicle);

                    return new Result<string> { Data = request.VehicleId!};
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;

                return result;
            }

            result.Success = false;
            result.ErrorMessage = "Bad request.";
            result.StatusCode = System.Net.HttpStatusCode.BadRequest;

            return result;
        }
    }
}
