using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class ItHasntBeen24HoursException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public ItHasntBeen24HoursException()
    {
        ErrorMessage = "It hasn't been 24 hours yet";
    }

    public ItHasntBeen24HoursException(string? message) : base(message)
    {
        ErrorMessage = message;
    }

}
