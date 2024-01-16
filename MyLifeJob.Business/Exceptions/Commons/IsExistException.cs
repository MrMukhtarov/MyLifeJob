using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Commons;

public class IsExistException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public IsExistException()
    {
        ErrorMessage = typeof(T).Name + " Is already exist";
    }

    public IsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
