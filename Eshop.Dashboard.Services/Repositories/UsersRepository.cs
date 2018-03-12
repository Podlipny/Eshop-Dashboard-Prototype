using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eshop.Dashboard.Services.Repositories
{
  public class UsersRepository : BaseRepository, IUsersRepository
  {
    public UsersRepository(EshopDbContext context)
      : base(context)
    {
    }

    public User GetUser(Guid userId)
    {
      return _context.Users.FirstOrDefault(a => a.Id == userId);
    }

    public User FindByName(string username)
    {
      return _context.Users.FirstOrDefault(a => a.Username == username);
    }

    public IEnumerable<User> GetUsers(IEnumerable<Guid> usersIds)
    {
      throw new NotImplementedException();
    }

    public bool UserExists(Guid userId)
    {
      return _context.Users.Any(a => a.Id == userId);
    }

    public void CreateUser(User user)
    {
      user.Id = Guid.NewGuid();
      _context.Users.Add(user);
    }

    public void DeleteUser(User user)
    {
      throw new NotImplementedException();
    }

    public bool Save()
    {
      return (_context.SaveChanges() >= 0);
    }

    public void UpdateAuthor(User user)
    {
      throw new NotImplementedException();
    }
  }
}
