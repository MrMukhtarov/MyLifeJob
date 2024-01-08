using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class UserProfileUpdateInvalidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public UserProfileUpdateInvalidException()
    {
        ErrorMessage = "User Profile update invalid for some reason";
    }

    public UserProfileUpdateInvalidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
