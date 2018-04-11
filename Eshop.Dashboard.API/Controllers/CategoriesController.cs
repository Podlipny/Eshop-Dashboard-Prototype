using AutoMapper;
using Eshop.Dashboard.API.Helpers;
using Eshop.Dashboard.API.ViewModels;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Eshop.Dashboard.API.Controllers
{
  [Route("api/[controller]")]
  public class CategoriesController : Controller
  {
    private ICategoriesRepository _categoriesRepository { get; }
    private IUrlHelper _urlHelper;

    public CategoriesController(ICategoriesRepository categoriesRepository, IUrlHelper urlHelper)
    {
      _categoriesRepository = categoriesRepository;
      _urlHelper = urlHelper;
    }

    /// <summary>
    /// GET - category by id for client side
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetCategory")]
    public IActionResult Get(Guid id)
    {
      var categoryEntity = _categoriesRepository.GetCategory(id);
      if (categoryEntity == null)
      {
        return NotFound($"Category with id: {id} does not found!");
      }

      var categoryToReturn = Mapper.Map<CategoryDto>(categoryEntity);

      return Ok(categoryToReturn);
    }

    /// <summary>
    /// POST - Create category
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost(Name = "CreateCategory")]
    public IActionResult Create([FromBody] CategoryToCreateViewModel model)
    {
      if (model == null)
        return BadRequest();

      if (ModelState.IsValid)
      {
        var categoryEntity = Mapper.Map<Category>(model);
        _categoriesRepository.Create(categoryEntity);

        if (!_categoriesRepository.Save())
        {
          throw new Exception("Creating a category failed on save.");
        }

        var categoryToReturn = Mapper.Map<Category>(categoryEntity);

        return CreatedAtRoute("GetCategory", new { id = categoryToReturn.Id }, categoryToReturn);
      }

      // return 422 - !ModelState.IsValid
      return new UnprocessableModelStateObjectResult(ModelState);
    }

    /// <summary>
    /// DELETE - category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("{id}", Name = "DeleteCategory")]
    public IActionResult Delete(Guid id)
    {
      var categoryEntity = _categoriesRepository.GetCategory(id);
      if (categoryEntity == null)
      {
        return NotFound($"Category with id: {id} does not found!");
      }

      _categoriesRepository.Delete(categoryEntity);
      if (!_categoriesRepository.Save())
      {
        throw new Exception($"Deleting Category {id} failed on save.");
      }

      return NoContent();
    }
  }
}
