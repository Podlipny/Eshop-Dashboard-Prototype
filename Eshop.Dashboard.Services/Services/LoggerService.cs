using System;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Enums;
using Eshop.Dashboard.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Eshop.Dashboard.Services.Services
{
  /// <summary>
  /// Logger service for logging into file and database
  /// All levels are logged to file, Only Event and Error are logged to DB
  /// - this behavior can be changened in config file
  /// - NLog.config must be allowed to Copy to output directory
  /// </summary>
  public class LoggerService : ILoggerService
  {
    private readonly ILoggerRepository _loggerRepository;
    private readonly ILogger _logger;

    private readonly bool _logToDatabase;

    public LoggerService(ILoggerFactory loggerFactory, IConfiguration configuration, ILoggerRepository loggerRepository)
    {
      _loggerRepository = loggerRepository;
      _logger = loggerFactory.CreateLogger("Global file logger");

      _logToDatabase = bool.Parse(configuration["Logging:LogToDatabase"]);
    }

    public void LogInfo(string message)
    {
      if (_logToDatabase)
        _loggerRepository.Log(LogEventsEnum.Info, message);
      _logger.LogInformation(message);
    }

    public void LogDebug(string message)
    {
      if (_logToDatabase)
        _loggerRepository.Log(LogEventsEnum.Debug, message);
      _logger.LogDebug(message);
    }

    public void LogTrace(string message)
    {
      if (_logToDatabase)
        _loggerRepository.Log(LogEventsEnum.Trace, message);
      _logger.LogTrace(message);
    }

    public void LogError(string message)
    {
      _loggerRepository.Log(LogEventsEnum.Error, message);
      _logger.LogError(message);
    }

    public void LogError(Exception exception)
    {
      _loggerRepository.Log(LogEventsEnum.Debug, exception.Message);
      _logger.LogError(500, exception, exception.Message);
    }

    /// <summary>
    /// Logs information about user specific actions
    /// </summary>
    /// <param name="message">Describtion of the action</param>
    /// <param name="userId">User identificator</param>
    public void LogEvent(string message, Guid userId)
    {

      _loggerRepository.Log(LogEventsEnum.Debug, $"{message}: UserId: {userId}");
      _logger.LogInformation($"{message}: UserId: {userId}");
    }

    /// <summary>
    /// Logs information about user specific actions
    /// </summary>
    /// <param name="message">Describtion of the action</param>
    /// <param name="user">User object</param>
    public void LogEvent(string message, User user)
    {
      _loggerRepository.Log(LogEventsEnum.Debug, $"{message}: UserId: {user.Id}");
      _logger.LogInformation($"{message}: UserId: {user.Id}");
    }
  }
}
