using BlogApi.Api.Services;
using BlogApi.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlogApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;
        private IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            if (await _userService.FindByUserNameAsync(request.UserName) != null)
                return BadRequest();


            var user = new User();
            user.UserName = request.UserName;
            user.PasswordHash = request.Password;
            
            await _userService.CreateUserAsync(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var response = await _authService.CreateAccessTokenAsync(request.UserName, request.Password);
            if(response.Success == false)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Token);
        }
    }
}
