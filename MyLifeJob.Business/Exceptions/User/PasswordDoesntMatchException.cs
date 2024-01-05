using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class PasswordDoesntMatchException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }


    public PasswordDoesntMatchException()
    {
        ErrorMessage = "Password doesn`t match";
    }

    public PasswordDoesntMatchException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
