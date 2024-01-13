using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class LogOutFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public LogOutFailedException()
    {
        ErrorMessage = "Log Out Failed for some reason";
    }

    public LogOutFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
