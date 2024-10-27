using KleskaProject.Domain.Common.Models;

namespace KleskaProject.Domain.UserAggregate;
public class UserDetails : Entity<Guid>
{
    public UserDetails(Guid id, Address address, PhoneNumber number) : base(id)
    {
        UserId = id;
        Address = address;
        phoneNumber = number;
        IsActive = true;
    }

    public Guid UserId { get; private set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;

    public bool IsActive { get; private set; }

    public Address Address { get; set; }
    public PhoneNumber phoneNumber { get; set; }

    public void Deactivate()
    {
        IsActive = false;
    }

}

