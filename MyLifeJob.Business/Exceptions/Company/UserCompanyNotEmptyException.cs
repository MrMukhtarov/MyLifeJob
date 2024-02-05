using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Company;

public class UserCompanyNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; set; }

    public UserCompanyNotEmptyException()
    {
        ErrorMessage = "User Company not empty";
    }

    public UserCompanyNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
