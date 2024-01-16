using MyLifeJob.Core.Entity.Commons;

namespace MyLifeJob.Core.Entity;

public class CompanyIndustry : BaseEntity
{
    public Company Company { get; set; }
    public int CompanyId { get; set; }
    public Industry Industry { get; set; }
    public int IndustryId { get; set; }
}
