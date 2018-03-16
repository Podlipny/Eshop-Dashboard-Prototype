using System;
using System.Collections.Generic;
using System.Text;
using Eshop.Dashboard.Services.Helpers;

namespace Eshop.Dashboard.Services.Services
{
  public interface IPropertyMappingService
  {
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();

    bool ValidMappingExistsFor<TSource, TDestination>(string fields);
  }
}
