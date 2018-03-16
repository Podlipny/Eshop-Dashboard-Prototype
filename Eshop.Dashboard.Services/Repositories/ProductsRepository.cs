using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Dynamic.Core;

namespace Eshop.Dashboard.Services.Repositories
{
  public class ProductsRepository : BaseRepository, IProductsRepository
  {
    private IConfiguration _configuration;

    public ProductsRepository(EshopDbContext context, IConfiguration configuration)
      : base(context)
    {
      _configuration = configuration;
    }

    /// <summary>
    /// Gets single product by GuidId
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Product GetProduct(Guid productId)
    {
      return _context.Products.FirstOrDefault(a => a.Id == productId);
    }

    /// <summary>
    /// Gets collection of products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> GetProducts()
    {
      return _context.Products.Include(x => x.Category).ToList();
    }

    /// <summary>
    /// Gets collection of products by ProductResourceParameters
    /// </summary>
    /// <param name="productResourceParameters"></param>
    /// <returns></returns>
    public PagedList<Product> GetProducts(ProductResourceParameters productResourceParameters)
    {
      //var collectionBeforePaging = _context.Products.ApplySort(productResourceParameters.OrderBy, _propertyMappingService.GetPropertyMapping<AuthorDto, Author>());
      IQueryable<Product> collectionBeforePaging = _context.Products.Include(x => x.Category).OrderBy(productResourceParameters.OrderBy.Trim().ToLowerInvariant() + " ascending");
      
      if (!string.IsNullOrEmpty(productResourceParameters.SearchQuery))
      {
        // trim & ignore casing
        var searchQueryForWhereClause = productResourceParameters.SearchQuery.Trim().ToLowerInvariant();

        collectionBeforePaging = collectionBeforePaging.Where(a => a.Name.ToLowerInvariant().Contains(searchQueryForWhereClause)
                      || a.Category.Name.ToLowerInvariant().Contains(searchQueryForWhereClause)
                      || a.Description.ToLowerInvariant().Contains(searchQueryForWhereClause));
      }

      return PagedList<Product>.Create(collectionBeforePaging, productResourceParameters.PageNumber, productResourceParameters.PageSize);
    }

    public bool ProductExists(Guid productId)
    {
      return _context.Products.Any(a => a.Id == productId);
    }

    public void Create(Product product)
    {
      product.Id = Guid.NewGuid();

      _context.Products.Add(product);
    }

    public void Delete(Product product)
    {
      _context.Products.Remove(product);
    }

    public void Update(Product product)
    {
      throw new NotImplementedException();
    }
  }

}
