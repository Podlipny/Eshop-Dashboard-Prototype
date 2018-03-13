using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Dashboard.API.ViewModels.Users;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Eshop.Dashboard.API.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    public IConfiguration _configuration { get; }
    public IUsersRepository _userRepository { get; }


    public UsersController(IConfiguration configuration, IUsersRepository userRepository)
    {
      _configuration = configuration;
      _userRepository = userRepository;
    }
    // GET api/Auth
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "Auth controller " };
    }

    [HttpPost]
    public IActionResult Register([FromBody] RegisterViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = _userRepository.FindByName(model.Username);
        if (user != null)
        {
          return StatusCode((int)HttpStatusCode.Conflict, $"Username: {model.Username} already exist in database!");
        }

        user = Mapper.Map<User>(model);
        _userRepository.CreateUser(user);
      }
      return BadRequest();
    }

    [HttpPost]
    public IActionResult CreateToken([FromBody] LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = _userRepository.FindByName(model.Username);

        if (user != null)
        {
          //TODO: change to clasick satl verification 

          //var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        //  if (result.Succeeded)
        //  {
        //    // Create the token
        //    //var claims = new[]
        //    //{
        //    //  new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //    //  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    //  new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
        //    //};

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //      _configuration["Tokens:Issuer"],
        //      _configuration["Tokens:Audience"],
        //      claims,
        //      expires: DateTime.Now.AddMinutes(30),
        //      signingCredentials: creds);

        //    var results = new
        //    {
        //      token = new JwtSecurityTokenHandler().WriteToken(token),
        //      expiration = token.ValidTo
        //    };

        //    return Created("", results);
        //  }
        }
      }

      return BadRequest();
    }
  }
}
