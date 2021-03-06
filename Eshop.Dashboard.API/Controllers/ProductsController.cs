﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Eshop.Dashboard.API.Enums;
using Eshop.Dashboard.API.Helpers;
using Eshop.Dashboard.API.ViewModels.Products;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Repositories;
using Eshop.Dashboard.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Dashboard.API.Controllers
{
  /// <summary>
  /// Controller for application Dashboard informations
  /// </summary>  
  [Route("api/[controller]")]
  public class ProductsController : Controller
  {
    private IProductsRepository _productsRepository { get; }
    private ILoggerService _logger;
    private IUrlHelper _urlHelper;

    public ProductsController(IProductsRepository productsRepository, ILoggerService loggerService, IUrlHelper urlHelper)
    {
      _productsRepository = productsRepository;
      _logger = loggerService;
      _urlHelper = urlHelper;
    }

    //TODO: authorization - not allow customers to call this
    //[Authorize]
    [HttpGet("{id}", Name = "GetProduct")]
    public IActionResult Get(Guid id)
    {
      var productEntity = _productsRepository.GetProduct(id);
      if (productEntity == null)
      {
        return NotFound($"Product with id: {id} does not found!");
      }

      var productToReturn = Mapper.Map<ProductDto>(productEntity);

      return Ok(productToReturn);
    }

    //TODO: authorization - not allow customers to call this
    //[Authorize]
    [HttpGet(Name = "GetProducts")]
    public IActionResult Get(CollectionResourceParameters productResourceParameters)
    {
      _logger.LogTrace("GetProducts");

      var productsFromRepo = _productsRepository.GetProducts(productResourceParameters);
      var products = Mapper.Map<IEnumerable<ProductDto>>(productsFromRepo);

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

    [HttpPost(Name = "CreateProduct")]
    public IActionResult Create([FromBody] ProductToCreateViewModel model)
    {
      if (model == null)
        return BadRequest();

      if (ModelState.IsValid)
      {
        var productEntity = Mapper.Map<Product>(model);
        _productsRepository.Create(productEntity);

        if (!_productsRepository.Save())
        {
          throw new Exception("Creating a product failed on save.");
        }

        var productToReturn = Mapper.Map<Product>(productEntity);

        return CreatedAtRoute("GetProduct", new { id = productToReturn.Id }, productToReturn);
      }

      // return 422 - !ModelState.IsValid
      return new UnprocessableModelStateObjectResult(ModelState);
    }

    [Authorize]
    [HttpDelete("{id}", Name = "DeleteProduct")]
    public IActionResult Delete(Guid id)
    {
      var productEntity = _productsRepository.GetProduct(id);
      if (productEntity == null)
      {
        return NotFound();
      }

      _productsRepository.Delete(productEntity);
      if (!_productsRepository.Save())
      {
        throw new Exception($"Deleting product {id} failed on save.");
      }

      return NoContent();
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
