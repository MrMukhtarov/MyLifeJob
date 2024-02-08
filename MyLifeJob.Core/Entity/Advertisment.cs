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
    public string? Ability { get; set; }
    public string Text { get; set; }
    public string Requirement { get; set; }
    public string? Experience { get; set; }
    public string? Education { get; set; }
    public int ViewCount { get; set; }
    public Status Status { get; set; }
    public DateTime CreateDate { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
}
