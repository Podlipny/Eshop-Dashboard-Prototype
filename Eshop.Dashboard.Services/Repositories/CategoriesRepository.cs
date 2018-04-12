using System;
using System.Collections.Generic;
using System.Linq;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eshop.Dashboard.Services.Repositories
{
  public class CategoriesRepository : BaseRepository, ICategoriesRepository
  {
    private IConfiguration _configuration;
    private IPropertyMappingService _propertyMappingService;

    public CategoriesRepository(EshopDbContext context, IConfiguration configuration, IPropertyMappingService propertyMappingService)
      : base(context)
    {
      _configuration = configuration;
      _propertyMappingService = propertyMappingService;
    }

    public bool CategoryExists(Guid categoryId)
    {
      return _context.Categories.Any(a => a.Id == categoryId);
    }

    public IEnumerable<Category> GetCategories()
    {
      return _context.Categories.Include(x => x.Products).ToList();
    }

    public PagedList<Category> GetCategories(SortableCollectionResourceParameters categoriesResourceParameters)
    {
      throw new NotImplementedException();
    }

    public Category GetCategory(Guid categoryId)
    {
      return _context.Categories.FirstOrDefault(a => a.Id == categoryId);
    }

    public void Create(Category category)
    {
      category.Id = Guid.NewGuid();

      _context.Categories.Add(category);
    }

    public void Update(Category category)
    {
      // no code in this implementation - only for readability
    }

    public void Delete(Category category)
    {
      _context.Categories.Remove(category);
    }
  }
}
