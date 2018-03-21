using System;

namespace Eshop.Dashboard.Services.Dto
{
  public class CategoryDto
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentId { get; set; }
  }
}
