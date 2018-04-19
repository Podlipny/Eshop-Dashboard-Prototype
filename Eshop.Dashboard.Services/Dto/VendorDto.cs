using System;

namespace Eshop.Dashboard.Services.Dto
{
  public class VendorDto
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Director { get; set; }

    public int Ico { get; set; }

    public string Dic { get; set; }

    public ContactDto Contact { get; set; }
  }
}
