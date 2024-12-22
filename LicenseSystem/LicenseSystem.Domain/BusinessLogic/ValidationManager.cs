using LicenseSystem.Domain.Entities;
using LicenseSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Domain.BusinessLogic
{
    // LicenseSystem.Domain/BusinessLogic/ValidationManager.cs
    public class ValidationManager
    {
        private readonly ILicenseDAO _licenseDAO;

        public ValidationManager(ILicenseDAO licenseDAO)
        {
            _licenseDAO = licenseDAO ?? throw new ArgumentNullException(nameof(licenseDAO));
        }

        public string GenerateLicenseKey()
        {
            // Generar clave única de 25 caracteres
            var key = $"{Guid.NewGuid():N}".Substring(0, 25).ToUpper();
            return key;
        }

        public async Task<bool> ValidateLicenseKeyAsync(string licenseKey)
        {
            var license = await _licenseDAO.GetByKeyAsync(licenseKey);
            if (license == null)
                return false;

            license.CheckExpiration();
            return license.Status == License.LicenseStatus.Active;
        }

        public async Task<LicenseValidationResult> ValidateFullLicenseAsync(
            string licenseKey, string machineId)
        {
            return await _licenseDAO.ValidateAndActivateLicenseAsync(licenseKey, machineId);
        }
    }
}
