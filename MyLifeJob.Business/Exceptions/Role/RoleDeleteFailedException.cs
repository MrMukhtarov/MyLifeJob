using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Role;

public class RoleDeleteFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public RoleDeleteFailedException()
    {
        ErrorMessage = "Delete role failed for some reason";
    }

    public RoleDeleteFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
