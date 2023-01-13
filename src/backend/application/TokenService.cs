using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using common;
using interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace application;

public class TokenService : ITokenService
{
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JWTToken GenerateToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name, username.ToUpper()),
        };

        var secretBytes = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(secretBytes),
            SecurityAlgorithms.HmacSha256);

        var from = DateTime.Now;
        var till = DateTime.Now.AddDays(7);
                
        var token = new JwtSecurityToken(
            _configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            claims,
            from,
            till,
            signingCredentials);

        var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

        return new JWTToken(tokenJson, from, till);
    }
}