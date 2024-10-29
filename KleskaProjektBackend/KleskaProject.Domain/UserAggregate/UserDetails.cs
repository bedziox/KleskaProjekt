using KleskaProject.Domain.Common.Models;
using System.Text.Json.Serialization;

namespace KleskaProject.Domain.UserAggregate;
public class UserDetails : Entity<Guid>
{
    [JsonConstructor]
    public UserDetails() : base(Guid.NewGuid()) { }
    public UserDetails(PhoneNumber number) : base(Guid.NewGuid())
    {
        phoneNumber = number;
        IsActive = true;
    }

    public Guid UserId { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; }

    public PhoneNumber phoneNumber { get; set; }

    public void Deactivate()
    {
        IsActive = false;
    }

}

