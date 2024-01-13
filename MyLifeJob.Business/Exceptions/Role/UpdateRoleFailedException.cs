using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Role;

public class UpdateRoleFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public UpdateRoleFailedException()
    {
        ErrorMessage = "Role name update failed for some reason";
    }

    public UpdateRoleFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
