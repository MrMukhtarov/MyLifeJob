using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Role;

public class RoleNotFoundException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public RoleNotFoundException()
    {
        ErrorMessage = "Role not found";
    }

    public RoleNotFoundException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
