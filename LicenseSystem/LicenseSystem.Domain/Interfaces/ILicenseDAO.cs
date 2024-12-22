using LicenseSystem.Domain.Entities;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Domain.Interfaces
{
    // LicenseSystem.Domain/Interfaces/ILicenseDAO.cs
    public interface ILicenseDAO : IBaseDAO<License>
    {
        Task<License> GetByKeyAsync(string licenseKey);
        Task<IEnumerable<License>> GetByCustomerIdAsync(int customerId);
        Task<IEnumerable<License>> GetByProductIdAsync(int productId);
        Task<bool> ValidateLicenseAsync(string licenseKey, string machineId);
        Task<LicenseValidationResult> ValidateAndActivateLicenseAsync(string licenseKey, string machineId);
        Task<int> GetActivationCountAsync(int licenseId);
    }
}
