using FluentValidation;
using System.Runtime.InteropServices;
using VehicleReservation.Application.Intefaces.Entities;
using VehicleReservation.Domain.Entities;

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
                .WithMessage("EndTo is required.")
                .Must(BeAValidDate).WithMessage("Start date is required");


            RuleFor(x => x.ReservationDto.VehicleId)
                .NotNull()
                .NotEmpty()
                .WithMessage("VehicleId is required.")
                .Must(CheckKeyComposition).WithMessage("UniqueId needs to be in fomrat C<number>");

            RuleFor(m => new { m.ReservationDto.StartFrom, m.ReservationDto.EndTo })
                .Must(x => ReservationDurationIsValid(x.StartFrom, x.EndTo))
                .WithMessage("Duration for using the vehicle cannot be more than 2 hours.");

            RuleFor(x => x.ReservationDto.StartFrom)
                .Must(BeAValidDate).WithMessage("Start date is required");


            RuleFor(m => new { m.ReservationDto.StartFrom, m.ReservationDto.EndTo, m.ReservationDto.VehicleId })
                .Must(x => HasFreeVehicle(x.StartFrom, x.EndTo, x.VehicleId))
                .WithMessage("Sorry, there is no free vehicles at the moment.");
            
            RuleFor(x => x).Must(x => x.ReservationDto.EndTo > x.ReservationDto.StartFrom)
                .WithMessage("EndTime must greater than StartTime");

        }

        private bool CheckKeyComposition(string key)
        {
            var firstChar = key.Substring(0, 1);
            var secondChar = key.Substring(1, key.Length - 1);
            int number;
            bool res = int.TryParse(secondChar, out number);

            return firstChar.Equals("C") && res;
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
        private bool ReservationDurationIsValid(DateTime dateFrom, DateTime dateTo)
        {
            return !(dateTo - dateFrom > new TimeSpan(2, 0, 0));
        }
        private bool CheckAdvancedLeaveTime(DateTime startFrom)
        {
            if (startFrom - DateTime.Now < new TimeSpan(24, 0, 0))
            {
                return false;
            }
            return true;

        }

        private bool HasFreeVehicle(DateTime startFrom, DateTime endTo, string vehicleId)
        {
            var hasFreeVehicleForPeriod = _repositoryReservation.HasFreeVehicleForPeriod(startFrom, endTo, vehicleId);
            return hasFreeVehicleForPeriod == null || hasFreeVehicleForPeriod;
        }
    }
}