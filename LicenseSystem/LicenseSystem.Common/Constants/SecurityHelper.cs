using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Common.Exceptions
{
    public static class SecurityHelper
    {
        public static string GenerateLicenseKey(int length = 29)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[length];
                rng.GetBytes(bytes);

                return Convert.ToBase64String(bytes)
                    .Replace("/", "_")
                    .Replace("+", "-")
                    .Replace("=", "")
                    .Substring(0, length);
            }
        }

        public static string GetMachineFingerprint()
        {
            var fingerprint = new StringBuilder();

            // Identificadores únicos de la máquina
            fingerprint.Append(Environment.MachineName);
            fingerprint.Append(Environment.ProcessorCount);
            fingerprint.Append(Environment.OSVersion.VersionString);

            // Crear hash del fingerprint
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(fingerprint.ToString()));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
