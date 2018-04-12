using System;
using System.Collections.Generic;
using System.Text;
using Eshop.Dashboard.Services.Enums;

namespace Eshop.Dashboard.Services.Repositories
{
  /// <summary>
  /// Repository interface for logging into database
  /// </summary>
  public interface ILoggerRepository
  {
    void Log(LogEventsEnum eventType, string message);
  }
}
