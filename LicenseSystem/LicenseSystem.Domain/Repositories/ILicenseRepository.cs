using LicenseSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Domain.Interfaces
{
    public interface ILicenseRepository
    {
        Task<License> GetByIdAsync(int licenseId);
        Task<License> GetByKeyAsync(string licenseKey);
        Task<int> CreateAsync(License license);
        Task UpdateAsync(License license);
        Task<IEnumerable<License>> GetByCustomerIdAsync(int customerId);
        Task<bool> ValidateLicenseAsync(string licenseKey, string machineId);
        Task<LicenseValidationResult> ValidateAndActivateLicenseAsync(string licenseKey, string machineId);
    }

    public class LicenseValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public ValidationStatus Status { get; set; }

        public enum ValidationStatus
        {
            Valid,
            Invalid,
            Expired,
            MaxActivationsReached
        }
    }
}