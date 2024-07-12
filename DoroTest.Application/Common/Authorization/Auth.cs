using DoroTest.Application.Services.Login.Reponses.LoginUser;
using DoroTest.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DoroTest.Application.Common.Authorization;
public class Auth
{
    public static LoginDto GenerateToken(UserEntity user, string SecretKey)
    {
        var claims = new[]
                   {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.UserType)

            };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var now = DateTime.Now;
        var exp = now.AddDays(10).ToLocalTime(); 

        var token = new JwtSecurityToken
        (
            claims: claims,
            signingCredentials: credential,
            expires: exp
        );

        return new LoginDto
        {
            Authenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserName = user.UserName,
            UserType = user.UserType,
            CreateDate = now.ToString("yyyy-MM-dd HH:mm:ss"),
            ExpirationDate = exp.ToString("yyyy-MM-dd HH:mm:ss")
        };

    }

    public static LoginDto GenerateTokenFromRefresh(IEnumerable<Claim> claims, string SecretKey)
    {

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var now = DateTime.Now;
        var exp = now.AddDays(10).ToLocalTime(); 

        var token = new JwtSecurityToken
        (
            claims: claims,
            signingCredentials: credential,
            expires: exp
        );

        return new LoginDto
        {
            Authenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserName = claims.ToList()[2].Value,
            UserType = claims.ToList()[4].Value,
            CreateDate = now.ToString("yyyy-MM-dd HH:mm:ss"),
            ExpirationDate = exp.ToString("yyyy-MM-dd HH:mm:ss")
        };

    }

    public static string GenerateRefreshToken()
    {

        var randonNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randonNumber);

        return Convert.ToBase64String(randonNumber);
    }


    public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string SecretKey)
    {

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid Token");

        return principal;

    }


}
