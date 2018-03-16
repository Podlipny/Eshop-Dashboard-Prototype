﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Dashboard.Services.Helpers
{
  public class PropertyMapping<TSource, TDestination> : IPropertyMapping
  {
    public Dictionary<string, PropertyMappingValue> _mappingDictionary { get; private set; }
    public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
      _mappingDictionary = mappingDictionary;
    }
  }
}
