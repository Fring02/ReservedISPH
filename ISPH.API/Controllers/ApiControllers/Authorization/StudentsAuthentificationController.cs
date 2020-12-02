using ISPH.Core.DTO;
using ISPH.Core.DTO.Authorization;
using ISPH.Core.Interfaces.Authentification;
using ISPH.Core.Models;
using ISPH.Infrastructure.Services.TokenConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ISPH.API.Controllers.ApiControllers.Authorization 
{
    [Route("users/students/auth/")]
    [ApiController]
    public class StudentsAuthController : ControllerBase
    {
        private readonly IUserAuthentification<Student> _authRepos;
        private readonly TokenCreatingService<Student> _tokenService;
        private IConfiguration Configuration { get; }
        public StudentsAuthController(IUserAuthentification<Student> authRepos, IConfiguration config)
        {
            _authRepos = authRepos;
            Configuration = config;
            _tokenService = new StudentTokenService(_authRepos);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent(StudentDto st)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var student = new Student()
            {
                FirstName = st.FirstName,
                LastName = st.LastName,
                Email = st.Email,
                Role = "student",
            };
            if (await _authRepos.UserExists(student)) return BadRequest("This user already exists");
            student = await _authRepos.Register(student, st.Password);
            if (student == null) return BadRequest("Oops, failed to register");
            var identity = await _tokenService.CreateIdentity(st.Email, st.Password);
            string token = _tokenService.CreateToken(identity, out string identityName, Configuration);

            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetString("Id", student.StudentId.ToString());
            HttpContext.Session.SetString("Name", student.FirstName);
            HttpContext.Session.SetString("Role", student.Role);
            return Ok(new {token, name = identityName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginStudent(LoginDto st)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var student = await _tokenService.CreateIdentity(st.Email, st.Password);
            if (student == null) return Unauthorized("Username or password is incorrect");
            string token = _tokenService.CreateToken(student, out string identityName, Configuration);

            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetString("Id", student.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            HttpContext.Session.SetString("Name", student.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name).Value);
            HttpContext.Session.SetString("Role", student.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role).Value);
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
