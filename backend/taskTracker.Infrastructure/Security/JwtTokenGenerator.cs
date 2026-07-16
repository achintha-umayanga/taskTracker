using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using taskTracker.Application.Common.Interfaces;
using taskTracker.Domain.Entities;

namespace taskTracker.Infrastructure.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly IConfiguration _configuration;

  public JwtTokenGenerator(IConfiguration configuration)
  {
      _configuration = configuration;
  }

  public string GenerateToken(User user)
  {
    var jwtKey = _configuration["Jwt:Key"]!;
    var jwtIssuer = _configuration["Jwt:Issuer"]!;
    var jwtAudience = _configuration["Jwt:Audience"]!;
    var expiresInMinutes = double.Parse(_configuration["Jwt:ExpiresInMinutes"]!);

    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new Claim(ClaimTypes.Name, user.Email)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: jwtIssuer,
      audience: jwtAudience,
      claims: claims,
      expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
      signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}