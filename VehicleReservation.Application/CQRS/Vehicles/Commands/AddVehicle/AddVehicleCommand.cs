using AutoMapper;
using MediatR;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Domain.Entities;
using VehicleReservation.Dto;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommand : IRequest<Result<VehicleDto>>
    {
        public VehicleDto? Vehicle { get; set; }
    }
    public class AddVehicleCommandHanlder : IRequestHandler<AddVehicleCommand, Result<VehicleDto>>
    {
        private readonly IVehicleRepositoryAsync _vehicleRepository;
        private IMapper _mapper;
        public AddVehicleCommandHanlder(IVehicleRepositoryAsync vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<Result<VehicleDto>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            Result<VehicleDto> result = new();
            try
            {
                if (request.Vehicle != null)
                {
                    VehicleDto vehicle = new();

                    vehicle.Maker = request.Vehicle.Maker;
                    vehicle.Model = request.Vehicle.Model;
                    vehicle.UniqueId = request.Vehicle.UniqueId;

                    Vehicle entity = _mapper.Map<Vehicle>(vehicle);
                    var response = await _vehicleRepository.AddAsync(entity);

                    result.Success = true;
                    result.StatusCode = System.Net.HttpStatusCode.OK;
                    result.Data = vehicle;

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Success = false;
                return result;
            }

            result.ErrorMessage = "Bad request.";
            result.StatusCode = System.Net.HttpStatusCode.BadRequest;
            result.Success = false;
            return result;
        }
    }
}
