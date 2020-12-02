using ISPH.Core.Interfaces.Authentification;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ISPH.Infrastructure.Configuration;

namespace ISPH.Infrastructure.Services.TokenConfiguration
{
    public abstract class TokenCreatingService<T>
    {
       protected readonly IUserAuthentification<T> Repos;
        protected TokenCreatingService(IUserAuthentification<T> repos)
        {
            Repos = repos;
        }
        public string CreateToken(ClaimsIdentity identity, out string identityName, IConfiguration configuration)
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
            var encodedToken = handler.WriteToken(token);
            identityName = identity.Name;
            return encodedToken;
        }

        public abstract Task<ClaimsIdentity> CreateIdentity(string email, string password);
    }
}
