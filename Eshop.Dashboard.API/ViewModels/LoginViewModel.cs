using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Dashboard.API.ViewModels
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Username can't be empty!")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password can't be empty!")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
  }
}
