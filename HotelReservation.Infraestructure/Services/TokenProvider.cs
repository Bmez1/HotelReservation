using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Infraestructure.Security;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;

using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System.Text;

namespace HotelReservation.Infraestructure.Services;

internal sealed class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string Create(User user, HashSet<string> permissions)
    {
        string secretKey = configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
        };

        foreach (string permission in permissions)
        {
            claims.Add(new(CustomClaims.Permissions, permission));
        }


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}


