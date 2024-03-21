using MyLifeJob.Core.Entity.Commons;

namespace MyLifeJob.Core.Entity;

public class Text : BaseEntity
{
    public string Content { get; set; }
    public Advertisment Advertisment { get; set; }
    public int AdvertismentId { get; set; }
}
