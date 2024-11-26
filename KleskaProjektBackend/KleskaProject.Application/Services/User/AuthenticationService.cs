using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using KleskaProjekt.Domain.Common.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KleskaProject.Application.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPhoneNumberRepository _phoneNumberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthenticationService() { }
    public AuthenticationService(IUserRepository userRepository, IPhoneNumberRepository phoneNumberRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _phoneNumberRepository = phoneNumberRepository;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
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

    public async Task<Result<TokenDto>> LoginUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return Result<TokenDto>.Failure(new Error(HttpStatusCode.BadRequest, "Wrong email or password"));
        }
        TokenDto token = await CreateToken(user, true);
        return Result<TokenDto>.Success(token);
    }
    public Result<TokenDto> ValidateUser(TokenDto token)
    {
        JwtSecurityToken newToken = new JwtSecurityToken(token.AccessToken);
        if (newToken.ValidTo > DateTime.UtcNow)
        {
            return Result<TokenDto>.Success(token);
        }
        return Result<TokenDto>.Failure(new Error(HttpStatusCode.Unauthorized, "Token not valid, login again"));
    }
    public async Task<TokenDto> CreateToken(User user, bool populateExp)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;

        if (populateExp)
        {
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        }

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        user = _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return new TokenDto(accessToken, refreshToken);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var jwtSecret = _configuration.GetSection("JwtSecret");
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret["keytoken"]));
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

    }
    private List<Claim> GetClaims(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Email),
        };
        return claims;

    }
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        return new JwtSecurityToken(
            issuer: _configuration["JwtSettings:validIssuer"],
            audience: _configuration["JwtSettings:validAudience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: signingCredentials
        );
    }
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rn = RandomNumberGenerator.Create())
        {
            rn.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {

        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = _configuration.GetSection("JwtSecret");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey["keytoken"]))
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }

    public async Task<Result<TokenDto>> RefreshTokenAsync(TokenDto token)
    {

        var principal = GetPrincipalFromExpiredToken(token.AccessToken);
        var emailClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name); // name is email
        var email = String.Empty;
        User user = null;
        if (emailClaim != null)
        {
            email = emailClaim.Value;
        }
        if (!email.Equals(String.Empty))
        {
            user = await _userRepository.GetByEmail(email);
        }
        if (user == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return Result<TokenDto>.Failure(new Error(HttpStatusCode.Unauthorized, "Invalid token"));
        }
        return Result<TokenDto>.Success(await CreateToken(user, true));
    }
    public void SetTokensInsideCookie(TokenDto tokenDto, HttpContext context)
    {
        context.Response.Cookies.Append("accessToken", tokenDto.AccessToken, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddHours(2),
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });
        context.Response.Cookies.Append("refreshToken", tokenDto.RefreshToken, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });
    }
}


