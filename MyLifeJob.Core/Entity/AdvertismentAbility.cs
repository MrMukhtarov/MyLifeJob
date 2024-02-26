using MyLifeJob.Core.Entity.Commons;

namespace MyLifeJob.Core.Entity;

public class AdvertismentAbility : BaseEntity
{
    public Ability Ability { get; set; }
    public int AbilityId { get; set; }
    public Advertisment Advertisment { get; set; }
    public int AdvertismentId { get; set; }
}
