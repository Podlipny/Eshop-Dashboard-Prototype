using System;
using System.Collections.Generic;
using System.Linq;
using Eshop.Dashboard.Data;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Dto;
using Eshop.Dashboard.Services.Helpers;
using Eshop.Dashboard.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eshop.Dashboard.Services.Repositories
{
  public class VendorRepository : BaseRepository, IVendorRepository
  {
    private IConfiguration _configuration;
    private IPropertyMappingService _propertyMappingService;

    public VendorRepository(EshopDbContext context, IConfiguration configuration, IPropertyMappingService propertyMappingService)
      : base(context)
    {
      _configuration = configuration;
      _propertyMappingService = propertyMappingService;
    }

    public Vendor GetVendor(Guid vendorId)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Vendor> GetVendors()
    {
      throw new NotImplementedException();
    }

    public PagedList<Vendor> GetVendors(CollectionResourceParameters resourceParameters)
    {
      IQueryable<Vendor> collectionBeforePaging = _context.Vendors.Include(x => x.Contact).ApplySort(resourceParameters.OrderBy, _propertyMappingService.GetPropertyMapping<VendorDto, Vendor>());

      if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
      {
        // trim & ignore casing
        var searchQueryForWhereClause = resourceParameters.SearchQuery.Trim().ToLowerInvariant();

        collectionBeforePaging = collectionBeforePaging.Where(a => a.Name.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                || a.Dic.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                || a.Ico.ToString().ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                || a.Director.ToLowerInvariant().Contains(searchQueryForWhereClause))
                                                        .Where(c => c.Contact != null && (c.Contact.Address1.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                 || c.Contact.Address2.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                 || c.Contact.City.ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                 || c.Contact.Psc.ToString().ToLowerInvariant().Contains(searchQueryForWhereClause)
                                                                 || c.Contact.State.ToLowerInvariant().Contains(searchQueryForWhereClause)));
      }

      return PagedList<Vendor>.Create(collectionBeforePaging, resourceParameters.PageNumber, resourceParameters.PageSize);
    }

    public bool VendorExists(Guid vendorId)
    {
      throw new NotImplementedException();
    }

    public bool VendorExistsInCategory(Guid vendorId, Guid categoryId)
    {
      throw new NotImplementedException();
    }

    public void Create(Vendor vendor)
    {
      throw new NotImplementedException();
    }

    public void Update(Vendor vendor)
    {
      throw new NotImplementedException();
    }

    public void Delete(Vendor vendor)
    {
      throw new NotImplementedException();
    }
  }
}
