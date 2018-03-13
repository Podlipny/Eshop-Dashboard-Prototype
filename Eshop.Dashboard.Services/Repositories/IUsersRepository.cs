using Eshop.Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Dashboard.Services.Repositories
{
  public interface IUsersRepository : IBaseRepository
  {
    User GetUser(Guid userId);

    User FindByName(string username);

    IEnumerable<User> GetUsers();

    bool UserExists(Guid userId);

    void CreateUser(User user);

    void UpdateAuthor(User user);

    void DeleteUser(User user);
  }
}
