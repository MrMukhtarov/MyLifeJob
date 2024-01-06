using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class LoginFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public LoginFailedException()
    {
        ErrorMessage = "Login Failed Some Reason";
    }

    public LoginFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
