using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class UpdatePasswordInvalidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public UpdatePasswordInvalidException()
    {
        ErrorMessage = "Update Password invalid for some reason";
    }

    public UpdatePasswordInvalidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
