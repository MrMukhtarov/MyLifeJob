using MyLifeJob.Core.Entity.Commons;

namespace MyLifeJob.Core.Entity;

public class EmailToken : BaseEntity
{
    public DateTime Date { get; set; }
    public string Token { get; set; }
    public AppUser User { get; set; }
    public string UserId { get; set; }
}
