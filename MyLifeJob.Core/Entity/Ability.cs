using MyLifeJob.Core.Entity.Commons;

namespace MyLifeJob.Core.Entity;

public class Ability : BaseEntity
{
    public string Name { get; set; }
    public ICollection<AdvertismentAbility>? AdvertismentAbilities { get; set; }

}
