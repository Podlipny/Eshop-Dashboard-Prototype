﻿using System.ComponentModel.DataAnnotations;

namespace Eshop.Dashboard.API.ViewModels
{
  public class LogToCreateViewModel
  {
    /// <summary>
    /// Type of log level
    /// </summary>
    [Required]
    public int LogLevelId { get; set; }

    /// <summary>
    /// Log message
    /// </summary>
    [Required]
    public string Message { get; set; }
  }
}
