using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ISPH.Domain.Core.Configuration;
using ISPH.Domain.Interfaces.Core;
using ISPH.Domain.Interfaces.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ISPH.API.TokenConfiguration
{
    public static class TokenCreatingService<TUser, TId> where TUser : IUser<TId>
    {
        public static string CreateToken(ClaimsIdentity identity, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var token = new JwtSecurityToken(
                claims: identity.Claims,
                audience: AuthOptions.Audience,
                issuer: AuthOptions.Issuer,
                expires: DateTime.Now.AddHours(AuthOptions.Lifetime),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                );
            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        public static ClaimsIdentity CreateIdentity(TUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };
            return new ClaimsIdentity(claims);
        }
    }
}
