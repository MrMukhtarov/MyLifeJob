using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Role;

public class CreateRoleFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public CreateRoleFailedException()
    {
        ErrorMessage = "Role Create failed for some reason";
    }

    public CreateRoleFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
