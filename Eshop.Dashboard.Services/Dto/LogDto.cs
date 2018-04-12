using System;

namespace Eshop.Dashboard.Services.Dto
{
  public class LogDto
  {
    /// <summary>
    /// Type of log level
    /// </summary>
    public string EvenType { get; set; }

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedWhen { get; set; }

    /// <summary>
    /// Logged message
    /// </summary>
    public string Message { get; set; }
  }
}
