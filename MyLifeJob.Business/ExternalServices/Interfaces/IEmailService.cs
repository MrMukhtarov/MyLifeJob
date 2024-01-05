using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.ExternalServices.Interfaces;

public interface IEmailService
{
    void SendEmail(Message message);
}
