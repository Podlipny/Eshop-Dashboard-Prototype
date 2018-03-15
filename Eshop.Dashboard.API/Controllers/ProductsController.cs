using System;
using System.Collections.Generic;
using AutoMapper;
using Eshop.Dashboard.API.Helpers;
using Eshop.Dashboard.API.ViewModels.Products;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Dashboard.API.Controllers
{
  [Route("api/[controller]")]
  public class ProductsController : Controller
  {
    private IProductsRepository _productsRepository { get; }

    public ProductsController(IProductsRepository productsRepository)
    {
      _productsRepository = productsRepository;
    }

    [Authorize]
    [HttpGet(Name = "GetProducts")]
    public IActionResult Get()
    {
      var productsEntities = _productsRepository.GetProducts();
      var productsToReturn = Mapper.Map<IEnumerable<ProductDtoViewModel>>(productsEntities);

      return Ok(productsToReturn);
    }

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

  }
}
