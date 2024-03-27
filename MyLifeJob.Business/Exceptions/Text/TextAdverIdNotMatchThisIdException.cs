using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Text;

public class TextAdverIdNotMatchThisIdException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public TextAdverIdNotMatchThisIdException()
    {
        ErrorMessage = "The Text`s Advertisment id not match this Advertisment Id";
    }

    public TextAdverIdNotMatchThisIdException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
