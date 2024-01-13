using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class UserDoesNotHaveThisRoleException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public UserDoesNotHaveThisRoleException()
    {
        ErrorMessage = "user does not have this role";
    }

    public UserDoesNotHaveThisRoleException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
