

using D_APP_Core.Entities;
using D_APP_Core.Entities.ModelsInput;
using D_APP_Core.Entities.Program;
using D_APP_Core.Interfaces.IServices;
using D_APP_Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace D_APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region PROPERTIES
        private IUserService _userService;
        private readonly IConfiguration _configuration;
        #endregion

        #region CONSTRUCTER
        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        #endregion

        #region METHOD



        /// <summary>
        /// Register for new user
        /// </summary>
        /// <returns>
        /// 1: token if register successfully
        /// 2: user and msg
        /// </returns>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterInput account)
        {
            DateTime dateTime = account.UserDateOfBirth.ToDateTime(TimeOnly.MinValue);

            // You can also specify the time and DateTimeKind
            TimeOnly specificTime = TimeOnly.Parse("10:30:00");
            DateTimeKind dateTimeKind = DateTimeKind.Utc;  // Or DateTimeKind.Local

            dateTime = account.UserDateOfBirth.ToDateTime(specificTime, dateTimeKind);

            var user = _userService.Register(new User
            {
                UserFname = account.UserFname,

                UserLname = account.UserLname,

                UserEmail = account.UserEmail,

                UserName = account.UserName,

                UserDateOfBirth = dateTime,

                UserPhoneNumber = account.UserPhoneNumber,

                UserPassword = account.UserPassword
            });
            if (user.statusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(user);
            }

            var roles = new[] { "User" };
            var token = GenerateJwtToken(account.UserName, roles);
            user.data = token;
            return Ok(user);
        }

        /// <summary>
        /// Api login for user
        /// </summary>
        /// <param name="acc"> username and password of user</param>
        /// <returns>
        /// 201: a token with login successfully
        /// 401: rollback username and password
        /// </returns>
        [HttpPost("Login")]
        public IActionResult Login(LoginInput acc)
        {
            /*
             { // account user test
                "email": "your_username",
                "password": "Hoang123"
             }
            */
            var user = _userService.LoginService(acc.username, acc.password);
            if (user.statusCode == HttpStatusCode.NotFound)
            {
                return Unauthorized(user);
            }
            if (user.statusCode == HttpStatusCode.OK)
            {
                var roles = new[] { "User" };
                var token = GenerateJwtToken(acc.username, roles);
                user.data = token;
                return Ok(user);
            }
            return BadRequest(user);
        }

        /// <summary>
        /// Generate token with jwt by username and user role
        /// </summary>
        /// <param name="username">username of user</param>
        /// <param name="roles">roles of user</param>
        /// <returns></returns>
        private string GenerateJwtToken(string username, string[] roles)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[HttpGet("ValidateAccount")]
        //public IActionResult ValidateAccount(Guid IDUser)
        //{
        //    var user = _userService.ValidateAccount(IDUser);
        //    return Ok(user);
        //}

        [Authorize]
        [HttpGet("Profile")]
        public IActionResult getUserWithAuthor()
        {
            var username = User?.Identity?.Name;

            var user = _userService.getUserByUserName(username);
            if (user.statusCode == HttpStatusCode.OK)
            {
                return Ok(user);
            }
            return BadRequest(user);
        }


        [Authorize]
        [HttpPut("UpdateProfile")]
        public IActionResult UpdateProfile([FromBody]UserInfo userUpdate)
        {
            var username = User?.Identity?.Name;
            var res = _userService.updateProfile(username, userUpdate);
            return Ok(res);
        }
        #endregion
    }
}
