using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceManager.Common.Helpers
{
    public class HashHelper
    {
        public static string Hash(string input)
        {
            // Implementación de hashing (ej: BCrypt)
            return BCrypt.Net.BCrypt.HashPassword(input);
        }
    }
}
