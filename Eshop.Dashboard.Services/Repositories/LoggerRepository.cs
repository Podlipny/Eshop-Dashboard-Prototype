using System;
using System.Globalization;
using System.Linq;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Dto;
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
    private IPropertyMappingService _propertyMappingService;

    public LoggerRepository(EshopDbContext context, IPropertyMappingService propertyMappingService)
      : base(context)
    {
    }

    public void Log(LogEventsEnum eventType, string message)
    {
      var logEntity = new Log();
      logEntity.Id = Guid.NewGuid();
      logEntity.EvenType = eventType.GetStringValue();
      logEntity.CreatedWhen = DateTime.Now;
      logEntity.Message = message;

      _context.Logs.Add(logEntity);

      base.Save();
    }

    public PagedList<Log> Get(CollectionResourceParameters logResourceParameters)
    {
      IQueryable<Log> collectionBeforePaging = _context.Logs.OrderByDescending(o => o.CreatedWhen);

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
