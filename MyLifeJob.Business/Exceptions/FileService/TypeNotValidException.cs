using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.FileService;

public class TypeNotValidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public TypeNotValidException()
    {
        ErrorMessage = "FIle type is not valid";
    }

    public TypeNotValidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
