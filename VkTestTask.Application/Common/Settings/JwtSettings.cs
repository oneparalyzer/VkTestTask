using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VkTestTask.Application.Common.Settings;

public class JwtSettings
{
    public const string SectionName = "Auth";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public int TokenLifeTime { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
    }
}
