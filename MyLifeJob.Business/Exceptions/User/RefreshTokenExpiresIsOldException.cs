using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Exceptions.User;

public class RefreshTokenExpiresIsOldException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public RefreshTokenExpiresIsOldException()
    {
        ErrorMessage = "RefreshToken Rxpires Date is Old";
    }

    public RefreshTokenExpiresIsOldException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
