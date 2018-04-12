using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Eshop.Dashboard.API.Enums;
using Eshop.Dashboard.API.Helpers;
using Eshop.Dashboard.API.ViewModels;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Enums;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Repositories;
using Eshop.Dashboard.Services.Services;
using Microsoft.AspNetCore.Mvc;


namespace Eshop.Dashboard.API.Controllers
{
  /// <summary>
  /// Controller for logging information from clients
  /// </summary>  
  [Route("api/[controller]")]
  public class LogsController : Controller
  {
    private ILoggerRepository _loggerRepository { get; }
    private ILoggerService _loggerService { get; }
    private IUsersRepository _usersRepository { get; }
    private IUrlHelper _urlHelper;

    public LogsController(ILoggerRepository loggerRepository, ILoggerService loggerService, IUsersRepository usersRepository,  IUrlHelper urlHelper)
    {
      _loggerRepository = loggerRepository;
      _loggerService = loggerService;
      _usersRepository = usersRepository;
      _urlHelper = urlHelper;
    }

    /// <summary>
    /// GET: Returns log entries
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetLogs")]
    public IActionResult Get(LogViewModel requestModel)
    {
      var logResourceParameters =  Mapper.Map<CollectionResourceParameters>(requestModel);
      var logsFromRepo = _loggerRepository.Get(logResourceParameters, requestModel.LogLevelId);
      var logs = Mapper.Map<IEnumerable<LogDto>>(logsFromRepo);

      var previousPageLink = logsFromRepo.HasPrevious ? CreateProductsResourceUri(logResourceParameters, ResourceUriType.PreviousPage) : null;

      var nextPageLink = logsFromRepo.HasNext ? CreateProductsResourceUri(logResourceParameters, ResourceUriType.NextPage) : null;

      var paginationMetadata = new
      {
        previousPageLink = previousPageLink,
        nextPageLink = nextPageLink,
        totalCount = logsFromRepo.TotalCount,
        pageSize = logsFromRepo.PageSize,
        currentPage = logsFromRepo.CurrentPage,
        totalPages = logsFromRepo.TotalPages
      };

      Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
      return Ok(logs);
    }

    /// <summary>
    /// POST: Create log entrie
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost(Name = "PostLogs")]
    public IActionResult Post([FromBody]LogToCreateViewModel model)
    {
      if (model == null)
        return BadRequest();

      if (ModelState.IsValid)
      {
        try
        {
          if (model.EvenTypeId == (int)LogEventsEnum.Trace)
            _loggerService.LogTrace(model.Message);
          else if (model.EvenTypeId == (int)LogEventsEnum.Debug)
            _loggerService.LogDebug(model.Message);
          else if (model.EvenTypeId == (int)LogEventsEnum.Error)
            _loggerService.LogError(model.Message);
          else if (model.EvenTypeId == (int) LogEventsEnum.Event)
          {
            if (User.Identity.IsAuthenticated)
            {
              var email = User.FindFirst(ClaimTypes.NameIdentifier).Value;
              var user = _usersRepository.FindByEmail(email);
              _loggerService.LogEvent(model.Message, user);
            }
            else
              _loggerService.LogEvent(model.Message);
          }
          else if (model.EvenTypeId == (int)LogEventsEnum.Warning)
            _loggerService.LogWarning(model.Message);
          else
            _loggerService.LogInfo(model.Message);
        }
        catch (Exception ex)
        {
          _loggerService.LogError("Fatal erorr - LogsController: exception durring logging!");
          _loggerService.LogError(ex);
        }

        return NoContent();
      }

      // return 422 - !ModelState.IsValid
      return new UnprocessableModelStateObjectResult(ModelState);
    }

    private string CreateProductsResourceUri(CollectionResourceParameters logResourceParameters, ResourceUriType type)
    {
      switch (type)
      {
        case ResourceUriType.PreviousPage:
          return _urlHelper.Link("GetLogs",
            new
            {
              searchQuery = logResourceParameters.SearchQuery,
              pageNumber = logResourceParameters.PageNumber - 1,
              pageSize = logResourceParameters.PageSize
            });
        case ResourceUriType.NextPage:
          return _urlHelper.Link("GetLogs",
            new
            {
              searchQuery = logResourceParameters.SearchQuery,
              pageNumber = logResourceParameters.PageNumber + 1,
              pageSize = logResourceParameters.PageSize
            });
        case ResourceUriType.Current:
        default:
          return _urlHelper.Link("GetLogs",
            new
            {
              searchQuery = logResourceParameters.SearchQuery,
              pageNumber = logResourceParameters.PageNumber,
              pageSize = logResourceParameters.PageSize
            });
      }
    }
  }
}
