namespace WeatherAPI.Abstractions;
public class ResultRemove<TValue, TError>
{
    public readonly TValue? Value;
    public readonly TError? Error;

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    private ResultRemove(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = default;
    }

    private ResultRemove(TError error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    //happy path
    public static implicit operator ResultRemove<TValue, TError>(TValue value) => new(value);

    //error path
    public static implicit operator ResultRemove<TValue, TError>(TError error) => new(error);

    public ResultRemove<TValue, TError> Match(
        Func<TValue, ResultRemove<TValue, TError>> success,
        Func<TError, ResultRemove<TValue, TError>> failure)
    {
        if (IsSuccess)
        {
            return success(Value!);
        }
        return failure(Error!);
    }
}
