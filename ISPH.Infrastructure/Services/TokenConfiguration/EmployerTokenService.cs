﻿using ISPH.Core.Interfaces.Authentification;
using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Services.TokenConfiguration
{
   public class EmployerTokenService : TokenCreatingService<Employer>
    {
        public EmployerTokenService(IUserAuthentification<Employer> repos) : base(repos)
        {
        }

        public override async Task<ClaimsIdentity> CreateIdentity(string email, string password)
        {
            var employer = await _userAuthRepos.Login(email, password);
            if (employer == null) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employer.EmployerId.ToString()),
                new Claim(ClaimTypes.Name, employer.FirstName),
                new Claim(ClaimTypes.Email, employer.Email),
                new Claim(ClaimTypes.Role, employer.Role),
                new Claim(ClaimTypes.UserData, employer.Company.Name),
            };
            return new ClaimsIdentity(claims);
        }
    }
}
