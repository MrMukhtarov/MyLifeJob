using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.Requirement;

public class RequirementAdverTismentIdNotMatchThisIdException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public RequirementAdverTismentIdNotMatchThisIdException()
    {
        ErrorMessage = "Requirement Advertisment Id not match this id";
    }

    public RequirementAdverTismentIdNotMatchThisIdException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
