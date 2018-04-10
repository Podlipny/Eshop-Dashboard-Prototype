using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Eshop.Dashboard.API.Enums;
using Eshop.Dashboard.API.Helpers;
using Eshop.Dashboard.API.ViewModels.Users;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Dashboard.API.Controllers.Dashboard
{
  [Route("api/dashboard/[controller]")]
  public class UsersController : Controller
  {
    private IUsersRepository _userRepository { get; }
    private IUrlHelper _urlHelper;

    public UsersController(IUsersRepository userRepository, IUrlHelper urlHelper)
    {
      _userRepository = userRepository;
      _urlHelper = urlHelper;
    }

    [Authorize]
    [HttpGet(Name = "GetUsers")]
    public IActionResult Get(CollectionResourceParameters userResourceParameters)
    {
      var usersFromRepo = _userRepository.GetUsers(userResourceParameters);
      var users = Mapper.Map<IEnumerable<UserDtoViewModel>>(usersFromRepo);

      var previousPageLink = usersFromRepo.HasPrevious ? CreateUsersResourceUri(userResourceParameters, ResourceUriType.PreviousPage) : null;

      var nextPageLink = usersFromRepo.HasNext ? CreateUsersResourceUri(userResourceParameters, ResourceUriType.NextPage) : null;

      var paginationMetadata = new
      {
        previousPageLink = previousPageLink,
        nextPageLink = nextPageLink,
        totalCount = usersFromRepo.TotalCount,
        pageSize = usersFromRepo.PageSize,
        currentPage = usersFromRepo.CurrentPage,
        totalPages = usersFromRepo.TotalPages
      };
      Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
      return Ok(users);
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

    //TODO: move this to base class
    private string CreateUsersResourceUri(CollectionResourceParameters usersResourceParameters, ResourceUriType type)
    {
      switch (type)
      {
        case ResourceUriType.PreviousPage:
          return _urlHelper.Link("GetUsers",
            new
            {
              fields = usersResourceParameters.Fields,
              orderBy = usersResourceParameters.OrderBy,
              searchQuery = usersResourceParameters.SearchQuery,
              pageNumber = usersResourceParameters.PageNumber - 1,
              pageSize = usersResourceParameters.PageSize
            });
        case ResourceUriType.NextPage:
          return _urlHelper.Link("GetUsers",
            new
            {
              fields = usersResourceParameters.Fields,
              orderBy = usersResourceParameters.OrderBy,
              searchQuery = usersResourceParameters.SearchQuery,
              pageNumber = usersResourceParameters.PageNumber + 1,
              pageSize = usersResourceParameters.PageSize
            });
        case ResourceUriType.Current:
        default:
          return _urlHelper.Link("GetUsers",
            new
            {
              fields = usersResourceParameters.Fields,
              orderBy = usersResourceParameters.OrderBy,
              searchQuery = usersResourceParameters.SearchQuery,
              pageNumber = usersResourceParameters.PageNumber,
              pageSize = usersResourceParameters.PageSize
            });
      }
    }


  }
}
