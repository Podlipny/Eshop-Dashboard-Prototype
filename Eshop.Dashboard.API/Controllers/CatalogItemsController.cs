using System;
using System.Collections.Generic;
using AutoMapper;
using Eshop.Dashboard.API.Enums;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Dashboard.API.Controllers
{
  /// <summary>
  /// Controller for Client product informations
  /// </summary>
  [Route("api/categories/{categoryId}/[controller]")]
  public class CatalogItemsController : Controller
  {
    private IProductsRepository _productsRepository { get; }
    private IUrlHelper _urlHelper;

    public CatalogItemsController(IProductsRepository productsRepository, IUrlHelper urlHelper)
    {
      _productsRepository = productsRepository;
      _urlHelper = urlHelper;
    }

    [HttpGet("{id}", Name = "GetCatalogItem")]
    public IActionResult Get(Guid categoryId, Guid id)
    {
      // TODO: return client specific product information
      var productEntity = _productsRepository.GetProductInCategory(id, categoryId);
      if (productEntity == null)
      {
        return NotFound($"Product with id: {id} does not found!");
      }

      var productToReturn = Mapper.Map<ProductDtoViewModel>(productEntity);

      return Ok(productToReturn);
    }

    [HttpGet(Name = "GetCatalogItems")]
    public IActionResult Get(Guid categoryId, CollectionResourceParameters productResourceParameters)
    {
     var productsFromRepo = _productsRepository.GetProducts(productResourceParameters, categoryId);
      var products = Mapper.Map<IEnumerable<ProductDtoViewModel>>(productsFromRepo);

      var previousPageLink = productsFromRepo.HasPrevious ? CreateProductsResourceUri(productResourceParameters, ResourceUriType.PreviousPage) : null;

      var nextPageLink = productsFromRepo.HasNext ? CreateProductsResourceUri(productResourceParameters, ResourceUriType.NextPage) : null;

      var paginationMetadata = new
      {
        previousPageLink = previousPageLink,
        nextPageLink = nextPageLink,
        totalCount = productsFromRepo.TotalCount,
        pageSize = productsFromRepo.PageSize,
        currentPage = productsFromRepo.CurrentPage,
        totalPages = productsFromRepo.TotalPages
      };
      Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
      return Ok(products);
    }

    private string CreateProductsResourceUri(CollectionResourceParameters productResourceParameters, ResourceUriType type)
    {
      switch (type)
      {
        case ResourceUriType.PreviousPage:
          return _urlHelper.Link("GetCatalogItem",
            new
            {
              fields = productResourceParameters.Fields,
              orderBy = productResourceParameters.OrderBy,
              searchQuery = productResourceParameters.SearchQuery,
              pageNumber = productResourceParameters.PageNumber - 1,
              pageSize = productResourceParameters.PageSize
            });
        case ResourceUriType.NextPage:
          return _urlHelper.Link("GetCatalogItem",
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
          return _urlHelper.Link("GetCatalogItem",
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
