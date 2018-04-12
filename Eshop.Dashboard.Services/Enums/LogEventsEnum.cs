using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Dashboard.Services.Enums
{
    public enum LogEventsEnum
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
