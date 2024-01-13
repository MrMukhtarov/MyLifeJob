using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class RemoveRoleFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public RemoveRoleFailedException()
    {
        ErrorMessage = "User role delete failed for some reason";
    }

    public RemoveRoleFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
