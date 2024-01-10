using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class CurrentPaswordIsInvalidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public CurrentPaswordIsInvalidException()
    {
        ErrorMessage = "Current Passowrd is not match current user password";
    }

    public CurrentPaswordIsInvalidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
