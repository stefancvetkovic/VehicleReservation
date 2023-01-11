using AutoMapper;
using MediatR;
using VehicleReservation.Application.CQRS.Vehicles.Queries;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Dto;

namespace VehicleReservation.Application.CQRS.Reservations.Queries
{
    public class GetAllReservationsQuery : IRequest<Result<List<ReservationDto>>>
    {
        public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, Result<List<ReservationDto>>>
        {
            private readonly IReservationRepositoryAsync _reservationRepositoryAsync;
            private readonly IMapper _mapper;

            public GetAllReservationsQueryHandler(IReservationRepositoryAsync reservationRepositoryAsync, IMapper mapper)
            {
                _reservationRepositoryAsync = reservationRepositoryAsync;
                _mapper = mapper;
            }

            public async Task<Result<List<ReservationDto>>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
            {
                Result<List<ReservationDto>> result = new();
                try
                {
                    var response = await _reservationRepositoryAsync.GetAllAsync();

                    result.Data = _mapper.Map<List<ReservationDto>>(response);
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
