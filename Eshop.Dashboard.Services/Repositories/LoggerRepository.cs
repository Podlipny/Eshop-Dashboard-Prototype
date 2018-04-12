using System;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Enums;
using Eshop.Dashboard.Services.Helpers;

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
  }
}
