using System;
using System.Collections.Generic;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Helpers;

namespace Eshop.Dashboard.Services.Repositories
{
  public interface IProductsRepository : IBaseRepository
  {
    Product GetProduct(Guid productId);

    Product GetProductInCategory(Guid productId, Guid categoryId);

    IEnumerable<Product> GetProducts();

    PagedList<Product> GetProducts(CollectionResourceParameters productResourceParameters, Guid? categoryId = null);

    bool ProductExists(Guid productId);

    bool ProductExistsInCategory(Guid productId, Guid categoryId);

    void Create(Product product);

    void Update(Product product);

    void Delete(Product product);
  }
}
