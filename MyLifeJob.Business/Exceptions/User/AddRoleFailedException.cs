using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class AddRoleFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public AddRoleFailedException()
    {
        ErrorMessage = "User add role failed for some reason";
    }

    public AddRoleFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
