using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Helpers;
using System;
using System.Collections.Generic;

namespace Eshop.Dashboard.Services.Repositories
{
  public interface ICategoriesRepository : IBaseRepository
  {
    Category GetCategory(Guid categoryId);

    IEnumerable<Category> GetCategories();

    PagedList<Category> GetCategories(SortableCollectionResourceParameters categoriesResourceParameters);

    bool CategoryExists(Guid categoryId);

    void Create(Category category);

    void Update(Category category);

    void Delete(Category category);
  }

}
