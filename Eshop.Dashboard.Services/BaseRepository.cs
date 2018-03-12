using Eshop.Dashboard.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Dashboard.Services
{
  public class BaseRepository : IBaseRepository
  {
    protected readonly EshopDbContext _context;

    public BaseRepository(EshopDbContext context)
    {
      _context = context;
    }

    public bool Save()
    {
      return (_context.SaveChanges() >= 0);
    }
  }
}
