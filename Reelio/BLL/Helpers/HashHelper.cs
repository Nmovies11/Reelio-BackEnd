using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;

namespace BLL.Helpers
{
    /// <summary>
    /// Helper class for password hashing and verification using Argon2id.
    /// </summary>  
    public class HashHelper
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 500;


        /// <summary>
        /// Hashes a password using Argon2id with a generated salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password in the format: salt:hash.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the password is null or empty.</exception>

        public static string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

            var salt = GenerateSalt();
            var hash = GenerateHash(password, salt);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }


        /// <summary>
        /// Verifies a password against a hashed password.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="encryptedPassword">The hashed password in the format: salt:hash.</param>
        /// <returns>True if the password matches; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any argument is null or empty.</exception>

        public static bool Verify(string password, string encryptedPassword)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

            if (string.IsNullOrEmpty(encryptedPassword))
                throw new ArgumentNullException(nameof(encryptedPassword), "Encrypted password cannot be null or empty.");


            var parts = encryptedPassword.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);

            var newHash = GenerateHash(password, salt);

            return SlowEquals(hash, newHash);
        }

        private static byte[] GenerateSalt()
        {
            var salt = new byte[SaltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        private static byte[] GenerateHash(string password, byte[] salt)
        {
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.MemorySize = 8192;
            argon2.Iterations = Iterations;

            return argon2.GetBytes(HashSize);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null)
                return false;

            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }
    }
}
