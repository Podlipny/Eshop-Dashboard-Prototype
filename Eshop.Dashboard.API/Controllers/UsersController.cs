using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Dashboard.API.Helpers;
using Eshop.Dashboard.API.ViewModels.Users;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Dashboard.API.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private IUsersRepository _userRepository { get; }

    public UsersController(IUsersRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [Authorize]
    [HttpGet(Name = "GetUsers")]
    public IActionResult Get()
    {
      var usersEntities = _userRepository.GetUsers();
      var usersToReturn = Mapper.Map<IEnumerable<UserDtoViewModel>>(usersEntities);
      
      return Ok(usersToReturn);
    }

    [Authorize]
    [HttpGet("{id}", Name = "GetUser")]
    public IActionResult Get(Guid id)
    {
      var userEntity = _userRepository.GetUser(id);
      if (userEntity == null)
      {
        return NotFound($"User with id: {id} does not exist!");
      }

      var userToReturn = Mapper.Map<UserDtoViewModel>(userEntity);

      return Ok(userToReturn);
    }

    [HttpPost(Name = "Register")]
    public IActionResult Register([FromBody] RegisterViewModel model)
    {
      if (model == null)
        return BadRequest();

      if (ModelState.IsValid)
      {
        var userEntity = _userRepository.FindByName(model.Username);
        if (userEntity != null)
        {
          return StatusCode((int)HttpStatusCode.Conflict, $"Username: {model.Username} already exist in database!");
        }

        userEntity = Mapper.Map<User>(model);
        _userRepository.CreateUser(userEntity);

        if (!_userRepository.Save())
        {
          throw new Exception("Creating an user failed on save.");
        }

        var userToReturn = Mapper.Map<UserDtoViewModel>(userEntity);

        return CreatedAtRoute("GetUser", new { id = userToReturn.Id }, userToReturn);

      }

      // return 422 - !ModelState.IsValid
      return new UnprocessableModelStateObjectResult(ModelState);
    }

    [Authorize]
    [HttpDelete("{id}", Name = "DeleteUser")]
    public IActionResult Delete(Guid id)
    {
      var userEntity = _userRepository.GetUser(id);
      if (userEntity == null)
      {
        return NotFound();
      }

      _userRepository.DeleteUser(userEntity);
      if (!_userRepository.Save())
      {
        throw new Exception($"Deleting user {id} failed on save.");
      }

      return NoContent();
    }

  }
}
