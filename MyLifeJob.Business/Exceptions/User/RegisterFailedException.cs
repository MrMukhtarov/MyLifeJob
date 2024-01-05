using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class RegisterFailedException : Exception, IBaseException
{

    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public RegisterFailedException()
    {
        ErrorMessage = "Register failed to some reason";
    }

    public RegisterFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
