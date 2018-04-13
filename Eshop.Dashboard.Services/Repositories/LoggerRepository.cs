using System;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Enums;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Services;

namespace Eshop.Dashboard.Services.Repositories
{
  /// <summary>
  /// Repository for logging into database
  /// </summary>
  public class LoggerRepository : BaseRepository, ILoggerRepository
  {
    public LoggerRepository(EshopDbContext context)
      : base(context)
    {
    }

    public void Log(LogLevelEnum levelType, string message)
    {
      var logEntity = new Log();
      logEntity.Id = Guid.NewGuid();
      logEntity.EvenType = levelType.GetStringValue();
      logEntity.CreatedWhen = DateTime.Now;
      logEntity.Message = message;

      _context.Logs.Add(logEntity);

      base.Save();
    }

    public PagedList<Log> Get(CollectionResourceParameters logResourceParameters, int? logLevel = null)
    {
      IQueryable<Log> collectionBeforePaging = _context.Logs.OrderByDescending(o => o.CreatedWhen);

      if(logLevel != null)
        collectionBeforePaging = collectionBeforePaging.Where(x => x.EvenType == ((LogLevelEnum)Enum.ToObject(typeof(LogLevelEnum), logLevel)).GetStringValue());

      if (!string.IsNullOrEmpty(logResourceParameters.SearchQuery))
      {
        // trim & ignore casing
        var searchQueryForWhereClause = logResourceParameters.SearchQuery.Trim().ToLowerInvariant();

        collectionBeforePaging = collectionBeforePaging.Where(a => a.EvenType.ToLowerInvariant().Contains(searchQueryForWhereClause)
                      || a.Message.ToLowerInvariant().Contains(searchQueryForWhereClause)
                      || a.CreatedWhen.ToLocalTime().ToString(CultureInfo.CurrentCulture).ToLowerInvariant().Contains(searchQueryForWhereClause));
      }

      return PagedList<Log>.Create(collectionBeforePaging, logResourceParameters.PageNumber, logResourceParameters.PageSize);
    }
  }
}
