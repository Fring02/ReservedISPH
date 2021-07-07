using System;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.API.Responses;
using ISPH.API.TokenConfiguration;
using ISPH.Domain.Dtos.Authorization;
using ISPH.Domain.Dtos.Users;
using ISPH.Domain.Interfaces.Core;
using ISPH.Domain.Interfaces.Services.Auth;
using ISPH.Domain.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ISPH.API.Controllers.Auth
{
    public class AuthenticationController<TUser, TRegisterUser, TId> : ControllerBase where TUser : IUser<TId> where TRegisterUser : IRegisterUserDto
    {
        private readonly IUserService<TUser, TId> _userService;
        private readonly IConfiguration _configuration;
        protected readonly IMapper _mapper;
        protected AuthenticationController(IUserService<TUser, TId> userService,
            IConfiguration configuration, IMapper mapper)
        {
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] TRegisterUser regUser)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var user = _mapper.Map<TUser>(regUser);
            try
            {
                await _userService.Register(user, regUser.Password);
                var identity = TokenCreatingService<TUser, TId>.CreateIdentity(user);
                var token = TokenCreatingService<TUser, TId>.CreateToken(identity, _configuration);
                return Ok(token);
            }
            catch (UserFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return this.ServerError("Failed to register");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto authUser)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            var user = await _userService.Login(authUser.Email, authUser.Password);
            if (user == null) return Unauthorized("Wrong email or password");
            var identity = TokenCreatingService<TUser, TId>.CreateIdentity(user);
            string token = TokenCreatingService<TUser, TId>.CreateToken(identity, _configuration);
            return Ok(token);
        }
    }
}