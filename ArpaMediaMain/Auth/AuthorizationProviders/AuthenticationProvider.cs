using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Services.Authorization
{
    public class AMResponseProvider
    {
		/// <summary>
		/// Creates password components (key and salt)
		/// </summary>
		/// <param name="password"></param>
		/// <param name="key"></param>
		/// <param name="salt"></param>
		public void CreatePasswordComponents(string password, out byte[] salt, out byte[] key)
		{
			using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, 20))
			{
				key = deriveBytes.GetBytes(20);  // derive a 20-byte key
				salt = deriveBytes.Salt;
			}
		}

		public bool VerifyPassword(
		   string attemptedPassword,
		   byte[] existingPasswordSalt,
		   byte[] existingPasswordKey)
		{
			using (var passwordKeyAsBytes = new Rfc2898DeriveBytes(attemptedPassword, existingPasswordSalt))
			{
				byte[] attemptedPasswordKey = passwordKeyAsBytes.GetBytes(20);
				return attemptedPasswordKey.SequenceEqual(existingPasswordKey);
			}
		}

	}
}
