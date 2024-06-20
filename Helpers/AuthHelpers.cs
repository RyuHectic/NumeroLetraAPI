using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NumeroLetraAPI.Helpers;

public class AuthHelpers
{
    private readonly IConfiguration _config;

    public AuthHelpers(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJWToken(int idusuario, string? nombre)
    {
        var jwtKey = _config["Jwt:Key"] ?? "";
        var jwtSubject = _config["Jwt:Subject"] ?? "";
        var jwtIssuer = _config["Jwt:Issuer"] ?? "";
        var jwtAudience = _config["Jwt:Audience"] ?? "";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var nowUtc = DateTime.UtcNow;
        var expirationDuration = TimeSpan.FromMinutes(10); // Adjust as needed
        var expirationUtc = nowUtc.Add(expirationDuration);

        var claims = new List<Claim> {
            new (ClaimTypes.NameIdentifier, idusuario.ToString() ?? "0"),
            new (ClaimTypes.Name, nombre ?? ""),
            new (JwtRegisteredClaimNames.Sub, jwtSubject),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(nowUtc).ToString(), ClaimValueTypes.Integer64),
            new (JwtRegisteredClaimNames.Exp, EpochTime.GetIntDate(expirationUtc).ToString(), ClaimValueTypes.Integer64),
            new (JwtRegisteredClaimNames.Iss, jwtIssuer),
            new (JwtRegisteredClaimNames.Aud, jwtAudience)
        };

        var jwtToken = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
