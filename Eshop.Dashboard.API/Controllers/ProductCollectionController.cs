using AutoMapper;
using Eshop.Dashboard.API.Enums;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Eshop.Dashboard.API.Controllers
{
  [Route("api/products")]
  public class ProductCollectionController : Controller
  {
    private IProductsRepository _productsRepository { get; }
    private IUrlHelper _urlHelper;

    public ProductCollectionController(IProductsRepository productsRepository, IUrlHelper urlHelper)
    {
      _productsRepository = productsRepository;
      _urlHelper = urlHelper;
    }

    //TODO: authorization - not allow customers to call this
    [Authorize]
    [HttpGet("{id}", Name = "GetProduct")]
    public IActionResult Get(Guid id)
    {
      var productEntity = _productsRepository.GetProduct(id);
      if (productEntity == null)
      {
        return NotFound($"Product with id: {id} does not found!");
      }

      var productToReturn = Mapper.Map<ProductDtoViewModel>(productEntity);

      return Ok(productToReturn);
    }

    //TODO: authorization - not allow customers to call this
    //[Authorize]
    [HttpGet(Name = "GetProducts")]
    public IActionResult Get(CollectionResourceParameters productResourceParameters)
    {
      var productsFromRepo = _productsRepository.GetProducts(productResourceParameters);
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
          return _urlHelper.Link("GetProducts",
            new
            {
              fields = productResourceParameters.Fields,
              orderBy = productResourceParameters.OrderBy,
              searchQuery = productResourceParameters.SearchQuery,
              pageNumber = productResourceParameters.PageNumber - 1,
              pageSize = productResourceParameters.PageSize
            });
        case ResourceUriType.NextPage:
          return _urlHelper.Link("GetProducts",
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
          return _urlHelper.Link("GetProducts",
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
