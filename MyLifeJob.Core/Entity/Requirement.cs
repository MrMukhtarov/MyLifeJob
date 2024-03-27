using MyLifeJob.Core.Entity.Commons;

namespace MyLifeJob.Core.Entity;

public class Requirement : BaseEntity
{
    public string Content { get; set; }
    public int AdvertismentId { get; set; }
    public Advertisment Advertisment { get; set; }
}
