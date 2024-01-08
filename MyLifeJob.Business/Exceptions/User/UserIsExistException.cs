using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class UserIsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public UserIsExistException()
    {
        ErrorMessage = "User Is Exist";
    }

    public UserIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
