using Eshop.Dashboard.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Dashboard.Services
{
  /// <summary>
  /// Interface for base repository class
  /// </summary>
  public interface IBaseRepository
  {
    bool Save();
  }
}
