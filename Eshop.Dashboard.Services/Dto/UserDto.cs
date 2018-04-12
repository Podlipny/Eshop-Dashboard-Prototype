using System;

namespace Eshop.Dashboard.Services.Dto
{
  public class UserDto
  {
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public int Ico { get; set; }

    public string Dic { get; set; }

    public int? Telephone { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string Psc { get; set; }

    public string City { get; set; }

    public string State { get; set; }

  }
}
