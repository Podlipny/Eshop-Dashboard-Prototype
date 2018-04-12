using System;

namespace Eshop.Dashboard.Services.Enums
{
  class StringValue : Attribute
  {
    public string Value { get; private set; }

    internal StringValue(string value)
    {
      this.Value = value;
    }
  }
}
