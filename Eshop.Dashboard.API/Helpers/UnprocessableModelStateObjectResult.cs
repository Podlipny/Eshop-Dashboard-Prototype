using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Eshop.Dashboard.API.Helpers
{
  public class UnprocessableModelStateObjectResult : ObjectResult
  {
    public UnprocessableModelStateObjectResult(ModelStateDictionary modelState)
      : base(new SerializableError(modelState))
    {
      if (modelState == null)
      {
        throw new ArgumentNullException(nameof(modelState));
      }

      StatusCode = 422;
    }
  }
}
