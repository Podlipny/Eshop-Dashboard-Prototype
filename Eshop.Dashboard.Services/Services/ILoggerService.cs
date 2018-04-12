using System;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Services.Services
{
  public interface ILoggerService
  {
    void LogInfo(string message);

    void LogDebug(string message);

    void LogTrace(string message);

    void LogError(string message);

    void LogError(Exception exception);

    void LogEvent(string message, Guid userId);

    void LogEvent(string message, User user);
  }
}
