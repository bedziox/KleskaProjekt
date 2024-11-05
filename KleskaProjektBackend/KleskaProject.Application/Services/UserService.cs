using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using KleskaProjekt.Domain.Common.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace KleskaProject.Domain.UserAggregate;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPhoneNumberRepository _phoneNumberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _keyToken = "KleskaProjektStringTokenThatIsLongEnoughForHmacSha512AlgorithmKeySize";

    public UserService() { }
    public UserService(IUserRepository userRepository, IPhoneNumberRepository phoneNumberRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _phoneNumberRepository = phoneNumberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> RegisterUserAsync(UserDto request)
    {
        var newUser = UserBuilder.BuildUser(request);
        if (await _userRepository.ExistsByEmailAsync(newUser.Email))
        {
            return Result<Guid>.Failure(new Error(HttpStatusCode.Conflict, "Email already in use. Please use a different email."));
        }
        _userRepository.Add(newUser);
        _phoneNumberRepository.Add(newUser.UserDetails.phoneNumber);
        await _unitOfWork.SaveChangesAsync();
        return Result<Guid>.Success(newUser.Id);
    }

    public async Task<Result<string>> LoginUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return Result<string>.Failure(new Error(HttpStatusCode.BadRequest, "Wrong email or password"));
        }
        string token = CreateToken(user);
        return Result<string>.Success(token);
    }
    public Result<string> ValidateUser(string token)
    {
        JwtSecurityToken newToken = new JwtSecurityToken(token);
        if (newToken.ValidTo > DateTime.UtcNow)
        {
            return Result<string>.Success(token);
        }
        return Result<string>.Failure(new Error(HttpStatusCode.Unauthorized, "Token not valid, login again"));
    }

    public string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _keyToken));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: cred);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}


