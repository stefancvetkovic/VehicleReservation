using AutoMapper;
using MediatR;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Dto;

namespace VehicleReservation.Application.CQRS.Vehicles.Queries
{
    public class GetAllVehiclesQuery : IRequest<Result<List<VehicleDto>>>
    {
        public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, Result<List<VehicleDto>>>
        {
            private readonly IVehicleRepositoryAsync _vehicleRepositoryAsync;
            private readonly IMapper _mapper;

            public GetAllVehiclesQueryHandler(IVehicleRepositoryAsync vehicleRepositoryAsync, IMapper mapper)
            {
                _vehicleRepositoryAsync = vehicleRepositoryAsync;
                _mapper = mapper;
            }

            public async Task<Result<List<VehicleDto>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
            {
                Result<List<VehicleDto>> result = new();
                try
                {
                    var response = await _vehicleRepositoryAsync.GetAllAsync();

                    result.Data = _mapper.Map<List<VehicleDto>>(response);
                    result.Success = true;
                    result.StatusCode = System.Net.HttpStatusCode.OK;
                    return result;
                }
                catch (Exception ex)
                {
                    result.ErrorMessage = ex.Message;
                    result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    return result;
                }
            }
        }
    }
}