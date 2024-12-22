using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LicenseSystem.Common.Constants
{
    public static class ValidationHelper
    {
        public static class LicenseRules
        {
            public static bool IsValidExpirationDate(DateTime expirationDate)
                => expirationDate > DateTime.Now;

            public static bool IsValidActivationCount(int current, int maximum)
                => current >= 0 && current <= maximum;

            public static bool IsValidLicenseKey(string licenseKey)
                => !string.IsNullOrEmpty(licenseKey) && licenseKey.Length == 29;
        }

        public static class CustomerRules
        {
            public static bool IsValidCompanyName(string name)
                => !string.IsNullOrWhiteSpace(name) && name.Length >= 2 && name.Length <= 100;

            public static bool IsValidContactName(string name)
                => !string.IsNullOrWhiteSpace(name) && name.Length >= 2 && name.Length <= 100;
        }

        public static class ProductRules
        {
            public static bool IsValidProductCode(string code)
                => !string.IsNullOrWhiteSpace(code) && code.Length >= 3 && code.Length <= 50;

            public static bool IsValidVersion(string version)
                => !string.IsNullOrWhiteSpace(version) &&
                   Regex.IsMatch(version, @"^\d+\.\d+(\.\d+)?(\.\d+)?$");
        }
    }
}
