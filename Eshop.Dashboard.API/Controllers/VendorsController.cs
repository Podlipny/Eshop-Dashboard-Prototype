using System.Collections.Generic;
using AutoMapper;
using Eshop.Dashboard.API.Enums;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Repositories;
using Eshop.Dashboard.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Eshop.Dashboard.API.Controllers
{
  /// <summary>
  /// Controller for Vendors managment
  /// </summary>  
  [Route("api/[controller]")]
  public class VendorsController : Controller
  {
    private IVendorRepository _vendorsRepository { get; }
    private ILoggerService _logger;
    private IUrlHelper _urlHelper;

    public VendorsController(IVendorRepository vendorsRepository, ILoggerService loggerService, IUrlHelper urlHelper)
    {
      _vendorsRepository = vendorsRepository;
      _logger = loggerService;
      _urlHelper = urlHelper;
    }

    //[Authorize]
    [HttpGet(Name = "GetVendors")]
    public IActionResult Get(CollectionResourceParameters vendorResourceParameters)
    {
      var vendorsFromRepo = _vendorsRepository.GetVendors(vendorResourceParameters);
      var vendors = Mapper.Map<IEnumerable<VendorDto>>(vendorsFromRepo);

      var previousPageLink = vendorsFromRepo.HasPrevious
        ? CreateVendorsResourceUri(vendorResourceParameters, ResourceUriType.PreviousPage)
        : null;

      var nextPageLink = vendorsFromRepo.HasNext
        ? CreateVendorsResourceUri(vendorResourceParameters, ResourceUriType.NextPage)
        : null;

      var paginationMetadata = new
      {
        previousPageLink = previousPageLink,
        nextPageLink = nextPageLink,
        totalCount = vendorsFromRepo.TotalCount,
        pageSize = vendorsFromRepo.PageSize,
        currentPage = vendorsFromRepo.CurrentPage,
        totalPages = vendorsFromRepo.TotalPages
      };
      Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
      return Ok(vendors);
    }

    private string CreateVendorsResourceUri(CollectionResourceParameters productResourceParameters, ResourceUriType type)
    {
      switch (type)
      {
        case ResourceUriType.PreviousPage:
          return _urlHelper.Link("GetVendors",
            new
            {
              fields = productResourceParameters.Fields,
              orderBy = productResourceParameters.OrderBy,
              searchQuery = productResourceParameters.SearchQuery,
              pageNumber = productResourceParameters.PageNumber - 1,
              pageSize = productResourceParameters.PageSize
            });
        case ResourceUriType.NextPage:
          return _urlHelper.Link("GetVendors",
            new
            {
              fields = productResourceParameters.Fields,
              orderBy = productResourceParameters.OrderBy,
              searchQuery = productResourceParameters.SearchQuery,
              pageNumber = productResourceParameters.PageNumber + 1,
              pageSize = productResourceParameters.PageSize
            });
        case ResourceUriType.Current:
        default:
          return _urlHelper.Link("GetVendors",
            new
            {
              fields = productResourceParameters.Fields,
              orderBy = productResourceParameters.OrderBy,
              searchQuery = productResourceParameters.SearchQuery,
              pageNumber = productResourceParameters.PageNumber,
              pageSize = productResourceParameters.PageSize
            });
      }
    }
  }
}
