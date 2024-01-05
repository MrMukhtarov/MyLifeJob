using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class EmailAlreadyExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public EmailAlreadyExistException()
    {
        ErrorMessage = "This Email has already exist";
    }
    public EmailAlreadyExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
