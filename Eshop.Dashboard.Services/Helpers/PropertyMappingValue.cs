﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Dashboard.Services.Helpers
{
  /// <summary>
  /// Defines mapping parametres for originated property
  /// </summary>
  public class PropertyMappingValue
  {
    public IEnumerable<string> DestinationProperties { get; private set; }
    public bool Revert { get; private set; }

    public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
    {
      DestinationProperties = destinationProperties;
      Revert = revert;
    }
  }
}
