using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleReservation.Application.Exceptions;
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
                result.ErrorMessage= ex.Message;
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }
    }
}
