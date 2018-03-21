using System;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Dashboard.API.ViewModels
{
  public class CategoryToCreateViewModel
  {
    [Required(ErrorMessage = "Category name can't be empty!")]
    [MaxLength(255, ErrorMessage = "Category name must be at least 255 characters!")]
    public string Name { get; set; }

    public Guid? ParentId { get; set; }
  }
}
