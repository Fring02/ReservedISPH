using ISPH.Core.DTO;
using ISPH.Core.DTO.Authorization;
using ISPH.Core.Interfaces.Authentification;
using ISPH.Core.Interfaces.Repositories;
using ISPH.Core.Models;
using ISPH.Infrastructure.Services.TokenConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ISPH.API.Controllers.ApiControllers.Authorization
{
    [Route("users/employers/auth/")]
    [ApiController]
    public class EmployersAuthController : ControllerBase
    {
        private readonly IUserAuthentification<Employer> _authRepos;
        private readonly ICompanyRepository _companyRepos;
        private readonly TokenCreatingService<Employer> _tokenService;
        private IConfiguration Configuration { get; }
        public EmployersAuthController(IUserAuthentification<Employer> authRepos, IConfiguration config, ICompanyRepository companyRepos)
        {
            _authRepos = authRepos;
            Configuration = config;
            _companyRepos = companyRepos;
            _tokenService = new EmployerTokenService(_authRepos);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterEmployer(EmployerDto em)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var employer = new Employer()
            {
                FirstName = em.FirstName,
                LastName = em.LastName,
                Email = em.Email,
                Role = "employer",
                CompanyId = em.CompanyId
            };
            var company = await _companyRepos.GetById(em.CompanyId);
            if (company == null) return BadRequest("Such company doesn't exist");
            if (await _authRepos.UserExists(employer)) return BadRequest("This user already exists");
            employer = await _authRepos.Register(employer, em.Password);
            if (employer == null) return BadRequest("Failed to register");
            var identity = await _tokenService.CreateIdentity(em.Email, em.Password);
            string token = _tokenService.CreateToken(identity, out string identityName, Configuration);

            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetString("Id", employer.EmployerId.ToString());
            HttpContext.Session.SetString("Name", employer.FirstName);
            HttpContext.Session.SetString("Role", employer.Role);
            return Ok(new {token, name = identityName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginEmployer(LoginDto em)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var employer = await _tokenService.CreateIdentity(em.Email, em.Password);
            if (employer == null) return Unauthorized("Username or password is incorrect");
            string token = _tokenService.CreateToken(employer, out string identityName, Configuration);


            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetString("Id", employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            HttpContext.Session.SetString("Name", employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name).Value);
            HttpContext.Session.SetString("Role", employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role).Value);
            return Ok(new {token, name = identityName });
        }

        [HttpPost("signout")]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return LocalRedirect("~/home/main");
        }

    }
}
