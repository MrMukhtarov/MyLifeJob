using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class UserDeleteInvalidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public UserDeleteInvalidException()
    {
        ErrorMessage = "User Delete invalid for some reason";
    }

    public UserDeleteInvalidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
