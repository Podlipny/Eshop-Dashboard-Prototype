using System;
using System.Collections.Generic;
using System.Text;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Helpers;

namespace Eshop.Dashboard.Services.Repositories
{
  public interface IProductsRepository : IBaseRepository
  {
    Product GetProduct(Guid productId);

    IEnumerable<Product> GetProducts();

    PagedList<Product> GetProducts(CollectionResourceParameters productResourceParameters);

    bool ProductExists(Guid productId);

    void Create(Product product);

    void Update(Product product);

    void Delete(Product product);
  }
}
