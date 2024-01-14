using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Exceptions.FileService;

public class ImagePathNullOrEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public ImagePathNullOrEmptyException()
    {
        ErrorMessage = "Image Path is null or empty";
    }

    public ImagePathNullOrEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
