using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class UserNotFoundException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public UserNotFoundException()
    {
        ErrorMessage = "User not found";
    }

    public UserNotFoundException(string? message) : base(message)
    {
        ErrorMessage = message;
    }

}
