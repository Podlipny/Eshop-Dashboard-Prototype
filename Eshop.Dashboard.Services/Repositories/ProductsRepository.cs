using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using Microsoft.Extensions.Configuration;

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

    public Product GetProduct(Guid productId)
    {
      return _context.Products.FirstOrDefault(a => a.Id == productId);
    }

    public IEnumerable<Product> GetProducts()
    {
      //TODO: add parametre search
      return _context.Products.ToList();
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
