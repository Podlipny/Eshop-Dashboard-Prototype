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
  /// All events are logged to file, Only Event and Error are logged to DB
  /// This behavior can be changened in config file
  /// </summary>
  public class LoggerService : ILoggerService
  {
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILoggerRepository _loggerRepository;
    private readonly ILogger _logger;

    private readonly bool _logAllToDb;

    public LoggerService(ILoggerFactory loggerFactory, IConfiguration configuration, ILoggerRepository loggerRepository)
    {
      _loggerFactory = loggerFactory;
      _loggerRepository = loggerRepository;
      _logger = _loggerFactory.CreateLogger("Global exception logger");

      _logAllToDb = bool.Parse(configuration["Logging:LogToDB"]);
    }

    public void LogInfo(string message)
    {
      if (_logAllToDb)
        _loggerRepository.Log(LogEventsEnum.Info, message);
      _logger.LogInformation(message);
    }

    public void LogDebug(string message)
    {
      if (_logAllToDb)
        _loggerRepository.Log(LogEventsEnum.Debug, message);
      _logger.LogDebug(message);
    }

    public void LogTrace(string message)
    {
      if (_logAllToDb)
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
    /// Loggs information about user specific actions
    /// </summary>
    /// <param name="message">Describtion of the action</param>
    /// <param name="userId">User identificator</param>
    public void LogEvent(string message, Guid userId)
    {

      _loggerRepository.Log(LogEventsEnum.Debug, $"{message}: UserId: {userId}");
      _logger.LogInformation($"{message}: UserId: {userId}");
    }

    /// <summary>
    /// Loggs information about user specific actions
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
