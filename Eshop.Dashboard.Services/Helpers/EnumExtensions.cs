using System;
using System.ComponentModel;
using System.Reflection;
using Eshop.Dashboard.Services.Enums;

namespace Eshop.Dashboard.Services.Helpers
{
  public static class EnumExtensions
  {
    public static string GetStringValue<T>(this T enumerationValue)
    where T : struct
    {
      Type type = enumerationValue.GetType();
      if (!type.IsEnum)
      {
        throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
      }

      //Tries to find a StringValue Attribute for a string representaion of enum
      MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
      if (memberInfo != null && memberInfo.Length > 0)
      {
        object[] attrs = memberInfo[0].GetCustomAttributes(typeof(StringValue), false);

        if (attrs != null && attrs.Length > 0)
        {
          //Pull out the string value from attribute
          return ((StringValue)attrs[0]).Value;
        }
      }
      //If we have no StringValue attribute, just return the ToString of the enum
      return enumerationValue.ToString();
    }
  }
}
