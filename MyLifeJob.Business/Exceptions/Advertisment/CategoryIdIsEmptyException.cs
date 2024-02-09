using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Advertisment;

public class CategoryIdIsEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; set; }

    public CategoryIdIsEmptyException()
    {
        ErrorMessage = "Category dont be empty";
    }

    public CategoryIdIsEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
