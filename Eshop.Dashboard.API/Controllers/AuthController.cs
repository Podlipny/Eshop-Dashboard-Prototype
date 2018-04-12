using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Eshop.Dashboard.API.Helpers;
using Eshop.Dashboard.API.ViewModels.Users;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Eshop.Dashboard.API.Controllers
{
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _userRepository;

    public AuthController(IConfiguration configuration, IUsersRepository userRepository)
    {
      _configuration = configuration;
      _userRepository = userRepository;
    }

    [HttpPost(Name = "CreateToken")]
    public IActionResult CreateToken([FromBody] LoginViewModel model)
    {
      if (model == null)
        return BadRequest();

      if (ModelState.IsValid)
      {
        var userEntity = _userRepository.FindByName(model.Username);

        if (userEntity != null)
        {
          var verified = PasswordHasher.Validate(model.Password, _configuration["Auth:Salt"], userEntity.Password);

          if (verified)
          {
            var userDto = Mapper.Map<UserDto>(userEntity);

            // Create the token
            var claims = new[]
            {
              //new Claim(JwtRegisteredClaimNames.Sub, JsonConvert.SerializeObject(userDto)),
              //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              //new Claim(JwtRegisteredClaimNames.UniqueName, userDto.Username)
              new Claim(ClaimTypes.NameIdentifier, userDto.Email, ClaimValueTypes.String),
              //new Claim(ClaimTypes.Role, userDto.Role, ClaimValueTypes.String),
              new Claim(ClaimTypes.Name, userDto.Username, ClaimValueTypes.String),
              new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(30).Second.ToString(), ClaimValueTypes.DaytimeDuration)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _configuration["Tokens:Issuer"],
              _configuration["Tokens:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            var results = new
            {
              token = new JwtSecurityTokenHandler().WriteToken(token),
              expiration = token.ValidTo
            };

            return Created("", results);
          }
          
          return Unauthorized();
        }
      }

      return new UnprocessableModelStateObjectResult(ModelState);
    }

  }
}
