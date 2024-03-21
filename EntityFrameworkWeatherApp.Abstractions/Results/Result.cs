using EntityFrameworkWeatherApp.Abstractions.Errors;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkWeatherApp.Abstractions.Results;
public class Result<TValue>
{
    public readonly TValue? Value;

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public Exception? GetException { get; private set; }
    public Error? GetError { get; private set; }
    public ValidationResult? GetValidationResult { get; private set; }

    public Dictionary<string, List<ProblemDetails>>? Exception = null;

    public Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Exception = null;
    }

    public Result(Exception e)
    {
        IsSuccess = false;
        Value = default;
        Exception = ReturnExceptions(e: e);
    }

    public Result(Error err)
    {
        IsSuccess = false;
        Value = default;
        Exception = ReturnExceptions(err: err);
    }

    public Result(ValidationResult vResult)
    {
        IsSuccess = false;
        Value = default;
        Exception = ReturnExceptions(vr: vResult);
    }

    public Result(Exception e, ValidationResult vResult)
    {
        IsSuccess = false;
        Value = default;
        Exception = ReturnExceptions(e: e, vr: vResult);
    }

    public Result(Error err, ValidationResult vResult)
    {
        IsSuccess = false;
        Value = default;
        Exception = ReturnExceptions(err: err, vr: vResult);
    }

    public Result(Exception e, Error err, ValidationResult vResult)
    {
        IsSuccess = false;
        Value = default;
        Exception = ReturnExceptions(e: e, err: err, vr: vResult);
    }

    //happy path
    public static implicit operator Result<TValue>(TValue value) => new(value);

    private Dictionary<string, List<ProblemDetails>> ReturnExceptions(
        Exception? e = null,
        Error? err = null,
        ValidationResult? vr = null)
    {
        var rDict = new Dictionary<string, List<ProblemDetails>>();

        if (e != null)
        {
            rDict.Add("Exceptipon", [new() {
                Status = 400,
                Title = "Exception",
                Detail = e.Message
            }]);
            GetException = e;
        }

        if (err != null)
        {
            rDict.Add("Error", [new() {
                Status = 400,
                Title = err.Code,
                Detail = err.ClientMessage
            }]);
            GetError = err;
        }

        if (vr != null)
        {
            var problemDetails = new List<ProblemDetails>();
            foreach (var item in vr.Errors)
            {
                problemDetails.Add(new()
                {
                    Status = null,
                    Title = item.PropertyName,
                    Detail = item.ErrorMessage
                });
            }

            rDict.Add("Validation", problemDetails);
            GetValidationResult = vr;
        }

        return rDict;
    }
}
