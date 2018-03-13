using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Eshop.Dashboard.Data
{
  public class PasswordHasher
  {
    public static string CreateHash(string value, string salt)
    {
      var valueBytes = KeyDerivation.Pbkdf2(password: value, salt: Encoding.UTF8.GetBytes(salt), prf: KeyDerivationPrf.HMACSHA512, iterationCount: 10000, numBytesRequested: 256 / 8);

      return Convert.ToBase64String(valueBytes);
    }

    public static bool Validate(string value, string salt, string hash) => CreateHash(value, salt) == hash;
  }
}
