using MyLifeJob.Core.Entity.Commons;

namespace MyLifeJob.Core.Entity;

public class Industry : BaseEntity
{
    public string Name { get; set; }
    public string Logo { get; set; }
    public ICollection<CompanyIndustry> CompanyIndustries { get; set; }
}
