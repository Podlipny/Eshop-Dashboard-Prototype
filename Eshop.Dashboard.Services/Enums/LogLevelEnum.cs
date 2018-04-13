namespace Eshop.Dashboard.Services.Enums
{
    public enum LogLevelEnum
    {
      [StringValue("INFO")]
      Info,

      [StringValue("DEBUG")]
      Debug,

      [StringValue("TRACE")]
      Trace,
      
      [StringValue("WARN")]
      Warning,

      [StringValue("EVENT")]
      Event,

      [StringValue("ERROR")]
      Error
    }
}
