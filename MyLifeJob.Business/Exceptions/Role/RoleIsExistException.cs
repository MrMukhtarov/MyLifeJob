using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Role;

public class RoleIsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public RoleIsExistException()
    {
        ErrorMessage = "Role is already exist";
    }

    public RoleIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
