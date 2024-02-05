using Domain.Entity.ErrorsHandler;

namespace Application.Error;

public class Result<T> where T : class 
{
    private Result(T? value) // Success Path
    {
        Value = value;
        IsSuccess = true;
        Errors = Errors.None;
    }

    private Result(Errors errors) //Failure path
    {
        Value = default;
        IsSuccess = false;
        Errors = errors;
    }

    public T? Value { get; }
    public Errors Errors { get; }
    private bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(Errors errors) => new(errors);
}
