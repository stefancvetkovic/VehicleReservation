using System.Net;

namespace VehicleReservation.Dto
{
    public class Result
    {
        public bool Success { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public List<FluentValidation.Results.ValidationResult>? Validation { get; set; }
        public void MergeResult(Result resultForMerge)
        {
            if (resultForMerge != null)
            {
                Success = Success && resultForMerge.Success;
                StatusCode = resultForMerge.StatusCode;
                ErrorMessage = resultForMerge.ErrorMessage;
                Validation = resultForMerge.Validation;
            }
        }
    }

    public class Result<TValidResult, TErrorResult> : Result
    {
        public TValidResult Data { get; set; } = default(TValidResult)!;
        public TErrorResult ErrorData { get; set; } = default(TErrorResult)!;

    }
    public class ErrorResult
    {
        public string? Message { get; set; }
        public string? FieldName { get; set; }
        public string? ErrorCode { get; set; }
    }

    public class Result<T> : Result<T, ErrorResult>
    {

    }
}