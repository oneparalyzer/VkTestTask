using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VkTestTask.Application.Common.Settings;
using VkTestTask.Application.Services.Interfaces;
using VkTestTask.Domain.AggregateModels.UserAggregate;

namespace VkTestTask.Application.Services.Implementations;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _authSettings;

    public JwtTokenService(IOptions<JwtSettings> authOptions)
    {
        _authSettings = authOptions.Value;
    }

    public string GenerateJwtToken(User user)
    {
        SymmetricSecurityKey securityKey = _authSettings.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Login),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new Claim("role", user.UserGroup.Code)
        };

        var token = new JwtSecurityToken(
            issuer: _authSettings.Issuer,
            audience: _authSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddSeconds(_authSettings.TokenLifeTime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
