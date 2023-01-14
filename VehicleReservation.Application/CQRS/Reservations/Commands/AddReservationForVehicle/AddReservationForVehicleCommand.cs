using AutoMapper;
using MediatR;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Domain.Entities;
using VehicleReservation.Dto;

namespace VehicleReservation.Application.CQRS.Reservations.Commands.AddReservationForVehicle
{
    public class AddReservationForVehicleCommand : IRequest<Result<ReservationDto>>
    {
        public ReservationDto? ReservationDto { get; set; }
    }

    public class AddReservationForVehicleCommandHandler : IRequestHandler<AddReservationForVehicleCommand, Result<ReservationDto>>
    {
        private readonly IReservationRepositoryAsync _reservationRepository;
        private IMapper _mapper;
        public AddReservationForVehicleCommandHandler(IReservationRepositoryAsync reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<Result<ReservationDto>> Handle(AddReservationForVehicleCommand request, CancellationToken cancellationToken)
        {
            Result<ReservationDto> result = new();
            try
            {
                if (request.ReservationDto == null)
                {
                    result.Success = true;
                    result.ErrorMessage = "Please check your reservation model body";
                    result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return result;
                }
                Reservation reservationEntity = _mapper.Map<Reservation>(request.ReservationDto);
                var response = await _reservationRepository.AddAsync(reservationEntity);

                result.Data = request.ReservationDto;
                result.Success = true;
                result.StatusCode = System.Net.HttpStatusCode.OK;

                return result;

            }
            catch (Exception ex)
            {
                result.Success = true;
                result.ErrorMessage = ex.Message;
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return result;
            }
        }
    }
}
