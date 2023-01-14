using FluentValidation;
using VehicleReservation.Application.Intefaces.Entities;

namespace VehicleReservation.Application.CQRS.Reservations.Commands.AddReservationForVehicle
{
    public class AddReservationForVehicleCommandValidator : AbstractValidator<AddReservationForVehicleCommand>
    {
        private readonly IReservationRepositoryAsync _repositoryReservation;
        public AddReservationForVehicleCommandValidator(IReservationRepositoryAsync repositoryReservation)
        {
            _repositoryReservation = repositoryReservation;

            RuleFor(x => x.ReservationDto.StartFrom).NotNull().Must(CheckAdvancedLeaveTime)
                .WithMessage("Sorry, we accept reservations at least 24h in advance. Please choose another Starting date and time")
                .NotEmpty()
                .NotNull()
                .WithMessage("StartFrom is required.");
            RuleFor(x => x.ReservationDto.EndTo)
                .NotNull()
                .NotEmpty()
                .WithMessage("EndTo is required.");
            RuleFor(x => x.ReservationDto.VehicleId)
                .NotNull()
                .NotEmpty()
                .WithMessage("VehicleId is required.")
                .Must(CheckKeyComposition).WithMessage("UniqueId needs to be in fomrat C<number>");
            RuleFor(m => new { m.ReservationDto.StartFrom, m.ReservationDto.EndTo})
                .Must(x => ReservationDurationIsValid(x.StartFrom, x.EndTo))
                .WithMessage("Duration for using the vehicle cannot be more than 2 hours.");
            RuleFor(m => new { m.ReservationDto.StartFrom, m.ReservationDto.EndTo, m.ReservationDto.VehicleId })
                .Must(x => HasFreeVehicle(x.StartFrom, x.EndTo, x.VehicleId))
                .WithMessage("Duration for using the vehicle cannot be more than 2 hours.");
        }

        private bool CheckKeyComposition(string key)
        {
            var firstChar = key.Substring(0);
            var secondChar = key.Substring(1, key.Length - 1);

            int number;
            bool res = int.TryParse(secondChar, out number);

            if (firstChar.Equals("C") && res)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ReservationDurationIsValid(DateTime dateFrom, DateTime dateTo)
        {
            if(dateTo - dateFrom > new TimeSpan(2, 0, 0))
            {
                return false;
            }

            return true;
        }
        private bool CheckAdvancedLeaveTime(DateTime startFrom)
        {
            if(DateTime.Now - startFrom < new TimeSpan(24, 0, 0))
            {
                return false;
            }
            return true;
        }

        private bool HasFreeVehicle(DateTime startFrom, DateTime endTo, string vehicleId)
        {
            var hasFreeVehicleForPeriod = _repositoryReservation.HasFreeVehicleForPeriod(startFrom, endTo, vehicleId);
            if (hasFreeVehicleForPeriod == null || hasFreeVehicleForPeriod)
            {
                return true;
            }
            return false;
        }
    }
}