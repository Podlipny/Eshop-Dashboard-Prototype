using System;
using System.Collections.Generic;
using System.Text;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Enums;
using Eshop.Dashboard.Services.Helpers;

namespace Eshop.Dashboard.Services.Repositories
{
  /// <summary>
  /// Repository interface for logging into database
  /// </summary>
  public interface ILoggerRepository
  {
    void Log(LogLevelEnum levelType, string message);

    PagedList<Log> Get(CollectionResourceParameters logResourceParameters, int? logLevel = null);
  }
}
