namespace KleskaProject.Domain.UserAggregate
{
    public class UserDto
    {
        public string FirstName { get; private set; } = String.Empty;
        public string LastName { get; private set; } = String.Empty;
        public string Email { get; private set; } = String.Empty;
        public string Password { get; private set; } = String.Empty;
        public UserDetails UserDetails { get; private set; }
        public Address Address { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
    }
}