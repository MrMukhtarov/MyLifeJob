using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.FileService;

public class SizeNotValidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public SizeNotValidException()
    {
        ErrorMessage = "FIle Size is not valid";
    }

    public SizeNotValidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
