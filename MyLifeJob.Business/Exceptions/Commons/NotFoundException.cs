using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Commons;

public class NotFoundException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public NotFoundException()
    {
        ErrorMessage = typeof(T).Name + " Not found";
    }

    public NotFoundException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
