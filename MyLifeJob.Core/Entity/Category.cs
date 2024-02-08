using MyLifeJob.Core.Entity.Commons;

namespace MyLifeJob.Core.Entity;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public ICollection<Advertisment> Advertisments { get; set; }
}
