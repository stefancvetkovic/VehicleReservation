using AutoMapper;
using MediatR;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Domain.Entities;
using VehicleReservation.Dto;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<Result<VehicleDto>>
    {
        public VehicleDto? Vehicle { get; set; }
    }
    public class UpdateVehicleCommandHanlder : IRequestHandler<UpdateVehicleCommand, Result<VehicleDto>>
    {
        private readonly IVehicleRepositoryAsync _vehicleRepository;
        private IMapper _mapper;
        public UpdateVehicleCommandHanlder(IVehicleRepositoryAsync vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<Result<VehicleDto>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            Result<VehicleDto> result = new();

            try
            {
                if (request.Vehicle != null)
                {
                    var entityForUpdate = await _vehicleRepository.GetById(request.Vehicle.UniqueId!);
                    Vehicle entity = _mapper.Map(request.Vehicle, entityForUpdate);

                    await _vehicleRepository.UpdateAsync(entity);

                    result.Data = _mapper.Map<VehicleDto>(entity);
                    result.Success = true;
                    result.StatusCode = System.Net.HttpStatusCode.OK;

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return result;
            }

            result.ErrorMessage = "BadRequest";
            result.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return result;
        }

        private static Result<Vehicle> ExceptionMethod(Result<Vehicle> result, Exception? ex = null, string customErrorMessage = "")
        {
            if (ex != null)
            {
               
            }
            else if (string.IsNullOrWhiteSpace(customErrorMessage))
            {
               
            }

            result.Success = false;

            return result;
        }
    }
}
