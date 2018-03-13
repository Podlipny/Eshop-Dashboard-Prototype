using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Eshop.Dashboard.Services.Repositories
{
  public class UsersRepository : BaseRepository, IUsersRepository
  {
    private IConfiguration _configuration;

    public UsersRepository(EshopDbContext context, IConfiguration configuration)
      : base(context)
    {
      _configuration = configuration;
    }

    public User GetUser(Guid userId)
    {
      return _context.Users.FirstOrDefault(a => a.Id == userId);
    }

    public User FindByName(string username)
    {
      return _context.Users.FirstOrDefault(a => a.Username == username);
    }

    public IEnumerable<User> GetUsers()
    {
      //TODO: add parametre search
      return _context.Users.ToList();
    }

    public bool UserExists(Guid userId)
    {
      return _context.Users.Any(a => a.Id == userId);
    }

    public void CreateUser(User user)
    {
      user.Id = Guid.NewGuid();
      user.Password = PasswordHasher.CreateHash(user.Password, _configuration["Auth:Salt"]);

      _context.Users.Add(user);
    }

    public void DeleteUser(User user)
    {
      _context.Users.Remove(user);
    }

    public void UpdateAuthor(User user)
    {
      throw new NotImplementedException();
    }
  }
}
