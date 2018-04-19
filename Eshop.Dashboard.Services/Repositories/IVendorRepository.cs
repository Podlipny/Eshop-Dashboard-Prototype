
using System;
using System.Collections.Generic;
using Eshop.Dashboard.Data.Entities;
using Eshop.Dashboard.Services.Helpers;

namespace Eshop.Dashboard.Services.Repositories
{
  public interface IVendorRepository
  {
    Vendor GetVendor(Guid vendorId);

    IEnumerable<Vendor> GetVendors();

    PagedList<Vendor> GetVendors(CollectionResourceParameters resourceParameters);

    bool VendorExists(Guid vendorId);

    bool VendorExistsInCategory(Guid vendorId, Guid categoryId);

    void Create(Vendor vendor);

    void Update(Vendor vendor);

    void Delete(Vendor vendor);
  }
}
