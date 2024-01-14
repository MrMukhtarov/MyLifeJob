using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Industry;

public class IndustryIsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public IndustryIsExistException()
    {
        ErrorMessage = "Industry is already exist";
    }

    public IndustryIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
