using KleskaProject.Domain.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace KleskaProject.Domain.UserAggregate;
public class User : ValueObject
{
    public Guid Id;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public string RefreshToken { get; set; } = String.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
    public UserDetails UserDetails { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return Email;
        yield return PasswordHash;
        yield return UserDetails;

    }

    public void Deactivate()
    {
        UserDetails.Deactivate();
    }

}

public class UserValidation
{
    public static bool ValidateEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }

    public static bool ValidatePassword(string password)
    {
        string regex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        return Regex.IsMatch(password, regex);
    }
}

public class UserBuilder
{
    public static User BuildUser(UserDto request)
    {
        if (!UserValidation.ValidateEmail(request.Email))
            throw new Exception("Incorrect email");
        if (!UserValidation.ValidatePassword(request.Password))
            throw new Exception("Password too weak, password should contain at least 8 characters, capital letter and special sign");
        User user = new User
        {
            Id = new Guid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, 17),
            UserDetails = new UserDetails(request.PhoneNumber)
        };
        return user;
    }
}
