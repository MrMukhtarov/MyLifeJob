using MyLifeJob.Core.Entity.Commons;

namespace MyLifeJob.Core.Entity;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string? Logo { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public string? Website { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public ICollection<CompanyIndustry> CompanyIndustries { get; set; }
}
