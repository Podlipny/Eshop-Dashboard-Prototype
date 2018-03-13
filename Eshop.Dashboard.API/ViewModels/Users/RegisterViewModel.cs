using System.ComponentModel.DataAnnotations;

namespace Eshop.Dashboard.API.ViewModels.Users
{
  public class RegisterViewModel
  {
    [Required(ErrorMessage = "Username can't be empty!")]
    public string Username { get; set; }

    [Required(ErrorMessage = "FistName can't be empty!")]
    [MaxLength(50)]
    public string FistName { get; set; }

    [Required(ErrorMessage = "LastName can't be empty!")]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Password can't be empty!")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }
  }
}
