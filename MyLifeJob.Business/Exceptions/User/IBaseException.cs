namespace MyLifeJob.Business.Exceptions.User;

public interface IBaseException
{
    public int StatusCode { get; }
    public string ErrorMessage { get; }
}
