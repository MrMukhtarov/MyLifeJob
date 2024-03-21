using MyLifeJob.Core.Entity.Commons;
using MyLifeJob.Core.Enums;

namespace MyLifeJob.Core.Entity;

public class Advertisment : BaseEntity
{
    public string Title { get; set; }
    public string City { get; set; }
    public decimal? Salary { get; set; }
    public string WorkGraphic { get; set; }
    public DateTime EndTime { get; set; }
    public ICollection<Text> Texts { get; set; }
    public string Requirement { get; set; }
    public string? Experience { get; set; }
    public Education? Education { get; set; }
    public int ViewCount { get; set; }
    public Status Status { get; set; }
    public DateTime CreateDate { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public Company Company { get; set; }
    public int CompanyId { get; set; }
    public State State { get; set; }
    public ICollection<AdvertismentAbility>? AdvertismentAbilities { get; set; }
}
