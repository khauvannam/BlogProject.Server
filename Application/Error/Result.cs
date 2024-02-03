using Domain.Entity.ErrorsHandler;

namespace Application.Error;

public class Result<T>
{
    public Result(T? value, bool isSuccess, Errors errors)
    {
        Value = value;
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public T? Value { get; }
    public Errors Errors { get; }
    private bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public static implicit operator Result<T>(T value) => new(value, true, Errors.None);

    public static implicit operator Result<T>(Errors errors) => new(default, false, errors);
}
