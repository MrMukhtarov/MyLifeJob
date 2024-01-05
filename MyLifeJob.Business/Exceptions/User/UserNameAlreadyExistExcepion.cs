using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class UserNameAlreadyExistExcepion : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public UserNameAlreadyExistExcepion()
    {
        ErrorMessage = "This Username has already exist";
    }

    public UserNameAlreadyExistExcepion(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
