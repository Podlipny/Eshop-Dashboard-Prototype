using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using System;
using System.Linq;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eshop.Dashboard.Services.Repositories
{
  public class UsersRepository : BaseRepository, IUsersRepository
  {
    private IConfiguration _configuration;
    private IPropertyMappingService _propertyMappingService;

    public UsersRepository(EshopDbContext context, IConfiguration configuration, IPropertyMappingService propertyMappingService)
      : base(context)
    {
      _configuration = configuration;
      _propertyMappingService = propertyMappingService;
    }

    public User GetUser(Guid userId)
    {
      return _context.Users.FirstOrDefault(a => a.Id == userId);
    }

    public User FindByName(string username)
    {
      return _context.Users.FirstOrDefault(a => a.Username == username);
    }

    public PagedList<User> GetUsers(CollectionResourceParameters userResourceParameters)
    {
      IQueryable<User> collectionBeforePaging = _context.Users.Include(x => x.Contact)
        .ApplySort(userResourceParameters.OrderBy, _propertyMappingService.GetPropertyMapping<UserDtoViewModel, User>());

      if (!string.IsNullOrEmpty(userResourceParameters.SearchQuery))
      {
        // trim & ignore casing
        var searchQueryForWhereClause = userResourceParameters.SearchQuery.Trim().ToLowerInvariant();

        collectionBeforePaging = collectionBeforePaging.Where(a => a.Username.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                   || a.Firstname.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                   || a.Lastname.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                   || a.Email.ToLowerInvariant().Contains(searchQueryForWhereClause))
                                                        .Where(c => c.Contact != null && (c.Contact.Address1.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                   || c.Contact.Address2.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                   || c.Contact.City.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                   || c.Contact.Psc.ToString().ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                   || c.Contact.State.ToLowerInvariant().Contains(searchQueryForWhereClause)));
      }
      return PagedList<User>.Create(collectionBeforePaging, userResourceParameters.PageNumber, userResourceParameters.PageSize);
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
