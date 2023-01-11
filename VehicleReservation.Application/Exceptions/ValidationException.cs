using FluentValidation.Results;

namespace VehicleReservation.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<ValidationResult> Errors { get; }

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<ValidationResult>();
        }

        public ValidationException(IEnumerable<ValidationResult> validationResults)
            : this()
        {
            foreach (var validationResult in validationResults)
            {
                Errors.Add(validationResult);
            }
        }
    }
}
