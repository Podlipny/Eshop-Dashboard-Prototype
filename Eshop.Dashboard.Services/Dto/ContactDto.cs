using System;

namespace Eshop.Dashboard.Services.Dto
{
  public class ContactDto
  {
    public Guid Id { get; set; }

    public int? Telephone { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public int Psc { get; set; }

    public string City { get; set; }

    public string State { get; set; }
  }
}
