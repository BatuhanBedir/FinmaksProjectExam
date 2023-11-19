using ExamProject.API.Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using ExamProject.API.Application.Settings;
using ExamProject.API.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExamProject.API.Application.Providers;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtOptions _jwtOptions;

    private readonly UserManager<AppUser> _userManager;
    public JwtTokenProvider(IOptions<JwtOptions> options, UserManager<AppUser> userManager)
    {
        _jwtOptions = options.Value;
        _userManager = userManager;
    }
    public async Task<string> GenerateToken(string userName, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim("UserName", userName),
            new Claim("Role", role)
        }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.DurationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
