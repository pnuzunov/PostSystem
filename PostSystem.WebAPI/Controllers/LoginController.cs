using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PostSystem.Business.DTO;
using PostSystem.Data;

namespace PostSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration config;

        public LoginController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserDto user)
        {
            UserDto resultUser = AuthenticateUser(user);

            if (resultUser != null)
            {
                var token = GenerateJsonWebToken(resultUser);

                return Ok(token);
            }

            return Unauthorized();
        }

        private object GenerateJsonWebToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDto AuthenticateUser(UserDto user)
        {
            using(UnitOfWork unitOfWork = new UnitOfWork())
            {
                var users = unitOfWork.UserRepository.GetAll();

                var foundUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

                if (foundUser != null)
                {
                    return new UserDto
                    {
                        Username = foundUser.Username
                    };
                }

                return null;
            }



        }
    }
}
