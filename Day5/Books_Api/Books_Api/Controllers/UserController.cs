using Books_Api.Entities.Entities;
using Books_Api.Services.Services.Interface;
using Books_Api.DTO;
using Books_Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Books_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly JwtHelper _jwtHelper;

        public UserController(IUserService userService, JwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddUser(User user)
        {
            await _userService.AddUser(user);
            return Ok("User Added!");
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] LoginReqDto dto)
        {
            var user = _userService.Login(dto.Email, dto.Password);

            if (user == null)
            {
                return NotFound("Please check your email & password");
            }

            var token = _jwtHelper.GetJwtToken(user);

            return Ok(new LoginResDto() { Email = user.Email, Name = user.Email, Role = user.Role, Token = token });
        }
    }
}
