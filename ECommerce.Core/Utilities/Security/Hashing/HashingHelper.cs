using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ECommerce.Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static string CreatePasswordHash(string password, string passwordSalt)
        {
            var passwordHashFromForm = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(passwordSalt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 961,
                numBytesRequested: 256 / 8));
            return passwordHashFromForm;
        }
        public static string CreatePasswordSalt()
        {
            byte[] randomBytes = new byte[256 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public static bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            var passwordHashFromForm = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(passwordSalt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 961,
                numBytesRequested: 256 / 8));

            return passwordHashFromForm.Equals(passwordHash);
        }
    }
}
