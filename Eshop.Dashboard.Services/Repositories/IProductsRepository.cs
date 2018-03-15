using System;
using System.Collections.Generic;
using System.Text;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Services.Repositories
{
  public interface IProductsRepository : IBaseRepository
  {
    Product GetProduct(Guid productId);

    IEnumerable<Product> GetProducts();

    bool ProductExists(Guid productId);

    void Create(Product product);

    void Update(Product product);

    void Delete(Product product);
  }
}
