using FluentValidation;
using System.Security.Cryptography.X509Certificates;
using VehicleReservation.Application.Intefaces.Entities;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
    {
        private readonly IVehicleRepositoryAsync _repositoryVehicle;
        public AddVehicleCommandValidator(IVehicleRepositoryAsync repositoryVehicle)
        {
            _repositoryVehicle = repositoryVehicle;

            RuleFor(x => x.Vehicle.Model).NotNull().WithMessage("Model is required.");
            RuleFor(x => x.Vehicle.Maker).NotNull().WithMessage("Maker is required.");
            RuleFor(x => x.Vehicle.UniqueId).NotNull().WithMessage("UniqueId is required").Must(CheckKeyComposition).WithMessage("UniqueId needs to be in fomrat C<number>");
            RuleFor(x => x.Vehicle.UniqueId).MustAsync((x, cancellation) => AlreadyInUse(x)).WithMessage("This id is already in use.");
        }

        private async Task<bool> AlreadyInUse(string key)
        {
            var entity = await _repositoryVehicle.GetById(key);
            if(entity == null)
            {
                return true;
            }
            var id = entity.UniqueId;

            if(key == id)
            {
                return true;
            }
            return false;
        }

        private bool CheckKeyComposition(string key)
        {
            var firstChar = key.Substring(0);
            var secondChar = key.Substring(1, key.Length-1);

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
    }
}