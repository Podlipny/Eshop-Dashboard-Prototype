using System;
using System.Collections.Generic;
using System.Linq;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Helpers;

namespace Eshop.Dashboard.Services.Services
{
  /// <summary>
  /// PropertyMappingService for coupling Dto property names to DB entity names
  /// </summary>
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

    private readonly Dictionary<string, PropertyMappingValue> _productPropertyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
    {
      { nameof(ProductDto.Id), new PropertyMappingValue(new List<string>() {"Id"}) },
      { nameof(ProductDto.Name), new PropertyMappingValue(new List<string>() {"Name"}) },
      { nameof(ProductDto.Description), new PropertyMappingValue(new List<string>() {"Description"}) },
      { nameof(ProductDto.Price), new PropertyMappingValue(new List<string>() {"Price"}) },
      { nameof(ProductDto.CategoryId), new PropertyMappingValue(new List<string>() {"Category.Id"}) },
      { nameof(ProductDto.CategoryName), new PropertyMappingValue(new List<string>() {"Category.Name"}) },
      { nameof(ProductDto.VendorId), new PropertyMappingValue(new List<string>() {"Vendor.Id"}) },
      { nameof(ProductDto.VendorName), new PropertyMappingValue(new List<string>() {"Vendor.Name"})},
      { nameof(ProductDto.State), new PropertyMappingValue(new List<string>() {"ProductState.Name"}) }
    };

    private readonly Dictionary<string, PropertyMappingValue> _userPropertyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
    {
      { nameof(UserDto.Id), new PropertyMappingValue(new List<string>() {"Id"}) },
      { nameof(UserDto.Username), new PropertyMappingValue(new List<string>() {"Username"}) },
      { nameof(UserDto.Email), new PropertyMappingValue(new List<string>() {"Email"}) },
      { nameof(UserDto.Firstname), new PropertyMappingValue(new List<string>() {"Firstname"}) },
      { nameof(UserDto.Lastname), new PropertyMappingValue(new List<string>() {"Lastname"}) },
      { nameof(UserDto.Ico), new PropertyMappingValue(new List<string>() {"Ico"}) },
      { nameof(UserDto.Dic), new PropertyMappingValue(new List<string>() {"Dic"}) },
      { nameof(UserDto.Telephone), new PropertyMappingValue(new List<string>() {"Contact.Telephone"})},
      { nameof(UserDto.Address1), new PropertyMappingValue(new List<string>() {"Contact.Address1"}) },
      { nameof(UserDto.Address2), new PropertyMappingValue(new List<string>() {"Contact.Address2"}) },
      { nameof(UserDto.Psc), new PropertyMappingValue(new List<string>() {"Contact.Psc"}) },
      { nameof(UserDto.City), new PropertyMappingValue(new List<string>() {"Contact.City"}) },
      { nameof(UserDto.State), new PropertyMappingValue(new List<string>() {"Contact.State"}) }
    };

    private readonly Dictionary<string, PropertyMappingValue> _vendorPropertyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
    {
      { nameof(VendorDto.Id), new PropertyMappingValue(new List<string>() { nameof(VendorDto.Id) }) },
      { nameof(VendorDto.Name), new PropertyMappingValue(new List<string>() { nameof(VendorDto.Name) }) },
      { nameof(VendorDto.Ico), new PropertyMappingValue(new List<string>() { nameof(VendorDto.Ico) }) },
      { nameof(VendorDto.Dic), new PropertyMappingValue(new List<string>() { nameof(VendorDto.Dic) }) },
      { nameof(VendorDto.Director), new PropertyMappingValue(new List<string>() { nameof(VendorDto.Director) }) },
      { nameof(ContactDto.Telephone), new PropertyMappingValue(new List<string>() {"Contact.Telephone"})},
      { nameof(ContactDto.Address1), new PropertyMappingValue(new List<string>() {"Contact.Address1"}) },
      { nameof(ContactDto.Address2), new PropertyMappingValue(new List<string>() {"Contact.Address2"}) },
      { nameof(ContactDto.Psc), new PropertyMappingValue(new List<string>() {"Contact.Psc"}) },
      { nameof(ContactDto.City), new PropertyMappingValue(new List<string>() {"Contact.City"}) },
      { nameof(ContactDto.State), new PropertyMappingValue(new List<string>() {"Contact.State"}) }
    };

    public PropertyMappingService()
    {
      _propertyMappings.Add(new PropertyMapping<ProductDto, Product>(_productPropertyMapping));
      _propertyMappings.Add(new PropertyMapping<UserDto, User>(_userPropertyMapping));
      _propertyMappings.Add(new PropertyMapping<VendorDto, Vendor>(_vendorPropertyMapping));
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
