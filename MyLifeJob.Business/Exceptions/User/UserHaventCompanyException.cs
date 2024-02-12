using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class UserHaventCompanyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; set; }

    public UserHaventCompanyException()
    {
        ErrorMessage = "User havent any company";
    }

    public UserHaventCompanyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
