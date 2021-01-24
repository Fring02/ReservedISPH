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
       protected readonly IUserAuthentification<T> _userAuthRepos;
        protected TokenCreatingService(IUserAuthentification<T> repos)
        {
            _userAuthRepos = repos;
        }
        public string CreateToken(ClaimsIdentity identity, out string identityName, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var token = new JwtSecurityToken(
                claims: identity.Claims,
                audience: AuthOptions.AUDIENCE,
                issuer: AuthOptions.ISSUER,
                expires: DateTime.Now.AddHours(AuthOptions.LIFETIME),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                );
            var handler = new JwtSecurityTokenHandler();
            identityName = identity.Name;
            return handler.WriteToken(token);
        }

        public abstract Task<ClaimsIdentity> CreateIdentity(string email, string password);
    }
}
