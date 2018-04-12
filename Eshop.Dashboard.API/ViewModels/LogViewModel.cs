namespace Eshop.Dashboard.API.ViewModels
{
    public class LogViewModel
    {
      /// <summary>
      /// Type of log level
      /// </summary>
      public int? LogLevelId { get; set; }

      public int PageNumber { get; set; } = 1;

      public int PageSize { get; set; } = 10;
      
      public string SearchQuery { get; set; }
    }
}
