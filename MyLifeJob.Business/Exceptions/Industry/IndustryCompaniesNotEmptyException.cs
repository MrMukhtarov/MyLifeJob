using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Industry;

public class IndustryCompaniesNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; set; }

    public IndustryCompaniesNotEmptyException()
    {
        ErrorMessage = "Industry Companies not empty";
    }

    public IndustryCompaniesNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
