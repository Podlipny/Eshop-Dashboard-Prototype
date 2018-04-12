using Eshop.Dashboard.Data.Entities;
using System;
using Eshop.Dashboard.Services.Helpers;

namespace Eshop.Dashboard.Services.Repositories
{
  public interface IUsersRepository : IBaseRepository
  {
    User GetUser(Guid userId);

    User FindByEmail(string email);

    User FindByName(string username);

    PagedList<User> GetUsers(CollectionResourceParameters productResourceParameters);

    bool UserExists(Guid userId);

    void CreateUser(User user);

    void UpdateAuthor(User user);

    void DeleteUser(User user);
  }
}
