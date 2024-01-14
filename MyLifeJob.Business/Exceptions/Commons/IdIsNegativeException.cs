using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Commons;

public class IdIsNegativeException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public IdIsNegativeException()
    {
        ErrorMessage = typeof(T).Name + " id is negative";
    }

    public IdIsNegativeException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
