using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Eshop.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IUserService _userService;
        private readonly IConfiguration configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            this.configuration = configuration;
        }
        [HttpPost]
        public IActionResult Post(string username, string password)
        {
            var user = _userService.LoginApiUser(username, password);
            if (user != null)
            {
                string[] tempstring = user.Password.Split("-");
                byte[] hashpassword = new byte[tempstring.Length];
                for (int i = 0; i < tempstring.Length; i++)
                    hashpassword[i] = Convert.ToByte(tempstring[i]);
                if (PasswordHash.VerifyHashedPasswordV2(hashpassword, password))
                {
                    var claims = new List<Claim>
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("UserName",user.Username)
                };
                    string key = configuration["JWtConfig:Key"];
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: configuration["JWtConfig:issuer"],
                        audience: configuration["JWtConfig:audience"],
                        expires: DateTime.Now.AddDays(int.Parse(configuration["JWtConfig:expires"])),
                        notBefore: DateTime.Now,
                        claims: claims,
                        signingCredentials: credentials
                        );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(jwtToken);

                }
                else
                {
                    return BadRequest("Not Allowed");
                }


            }
            else
            {
                return BadRequest("Not Allowed");
            }
        }
    }
}
