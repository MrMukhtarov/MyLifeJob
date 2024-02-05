using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Company;

public class UserNotSameException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; set; }

    public UserNotSameException()
    {
        ErrorMessage = "Company User not same this user";
    }

    public UserNotSameException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
