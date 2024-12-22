using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Common.Exceptions
{
    public class LicenseValidationException : Exception
    {
        public string LicenseKey { get; }
        public string ValidationError { get; }

        public LicenseValidationException(string message, string licenseKey, string validationError)
            : base(message)
        {
            LicenseKey = licenseKey;
            ValidationError = validationError;
        }
    }
}
