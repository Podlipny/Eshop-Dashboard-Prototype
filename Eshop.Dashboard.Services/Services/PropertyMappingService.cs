using System;
using System.Collections.Generic;
using System.Linq;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Helpers;

namespace Eshop.Dashboard.Services.Services
{
  public class PropertyMappingService : IPropertyMappingService
  {
    // - definuje nam kolekci property objektu k mappingu
    // - delame to kvuli tomu, ze nase entita ma DateOfBirth misto Age a FirstName Last name misto Name
    //   a jelikoz chceme sortovat podle name, tak musime udelat mapping mezi entitou a nasim DTO
    // - pokud by to nebylo nebudeme sortovat na urovni DB (ApplySort), ale az pomoci OrderBy na jiz
    //   nacetene kolekci
    //  private Dictionary<string, PropertyMappingValue> _authorPropertyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
    //    {
    //      {"Id", new PropertyMappingValue(new List<string>() {"Id"})},
    //      {"Genre", new PropertyMappingValue(new List<string>() {"Genre"})},
    //      {"Age", new PropertyMappingValue(new List<string>() {"DateOfBirth"}, true)},
    //      {"Name", new PropertyMappingValue(new List<string>() {"FirstName", "LastName"})}
    //    };

    private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

    Dictionary<string, PropertyMappingValue> productPropertyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
    {
      { "Id", new PropertyMappingValue(new List<string>() {"Id"})},
      {"Name", new PropertyMappingValue(new List<string>() {"Name"})},
      {"Description", new PropertyMappingValue(new List<string>() {"Description"})},
      {"Price", new PropertyMappingValue(new List<string>() {"Price"})},
      {"Category", new PropertyMappingValue(new List<string>() {"Category.Name"})}
    };

    public PropertyMappingService(IEnumerable<IPropertyMapping> propertyMappings)
    {
      _propertyMappings.Add(new PropertyMapping<ProductDtoViewModel, Product>(productPropertyMapping));
    }

    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
    {
      // get matching mapping
      var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
      if (matchingMapping.Count() == 1)
      {
        // prohledame mapping dictionary a poukud nejakou najdeme, tak ji vratime
        return matchingMapping.First()._mappingDictionary;
      }

      throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");
    }

    public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
    {
      var propertyMapping = GetPropertyMapping<TSource, TDestination>();
      if (string.IsNullOrWhiteSpace(fields))
      {
        return true;
      }

      // the string is separated by ",", so we split it.
      var fieldsAfterSplit = fields.Split(',');

      // run through the fields clauses
      foreach (var field in fieldsAfterSplit)
      {
        // trim
        var trimmedField = field.Trim();

        // remove everything after the first " " - if the fields 
        // are coming from an orderBy string, this part must be 
        // ignored
        var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal);
        var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

        // find the matching property
        if (!propertyMapping.ContainsKey(propertyName))
        {
          return false;
        }
      }

      return true;
    }
  }

}
