using LicenseSystem.DataAccess.Connection;
using LicenseSystem.Domain.Entities;
using LicenseSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LicenseSystem.DataAccess.DAOs.LicenseSystem.DataAccess.DAOs;

namespace LicenseSystem.DataAccess.DAOs
{
    public class LicenseDAO : BaseDAO, ILicenseDAO
    {
        public LicenseDAO(DatabaseConnection dbConnection, ILogger<LicenseDAO> logger)
            : base(dbConnection, logger)
        {
        }

        // Implementación de IBaseDAO<License>
        public async Task<int> CreateAsync(License license)
        {
            return await ExecuteScalarAsync<int>(
                "sp_CreateLicense",
                ("@CustomerId", license.CustomerId),
                ("@ProductId", license.ProductId),
                ("@LicenseTypeId", license.LicenseTypeId),
                ("@LicenseKey", license.LicenseKey),
                ("@ExpirationDate", license.ExpirationDate),
                ("@MaxActivations", license.MaxActivations));
        }

        public async Task UpdateAsync(License license)
        {
            await ExecuteNonQueryAsync(
                "sp_UpdateLicense",
                ("@LicenseId", license.LicenseId),
                ("@Status", license.Status.ToString()),
                ("@CurrentActivations", license.CurrentActivations),
                ("@Notes", license.Notes),
                ("@ModifiedDate", DateTime.Now));
        }

        public async Task<License> GetByIdAsync(int id)
        {
            return await ExecuteReaderAsync(
                "sp_GetLicenseById",
                MapToLicense,
                ("@LicenseId", id));
        }

        public async Task<IEnumerable<License>> GetAllAsync()
        {
            return await ExecuteReaderListAsync(
                "sp_GetAllLicenses",
                MapToLicense);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await ExecuteNonQueryAsync(
                "sp_DeleteLicense",
                ("@LicenseId", id));

            return result > 0;
        }

        // Implementación de ILicenseDAO
        public async Task<License> GetByKeyAsync(string licenseKey)
        {
            return await ExecuteReaderAsync(
                "sp_GetLicenseByKey",
                MapToLicense,
                ("@LicenseKey", licenseKey));
        }

        public async Task<IEnumerable<License>> GetByCustomerIdAsync(int customerId)
        {
            return await ExecuteReaderListAsync(
                "sp_GetLicensesByCustomerId",
                MapToLicense,
                ("@CustomerId", customerId));
        }

        public async Task<IEnumerable<License>> GetByProductIdAsync(int productId)
        {
            return await ExecuteReaderListAsync(
                "sp_GetLicensesByProductId",
                MapToLicense,
                ("@ProductId", productId));
        }

        public async Task<bool> ValidateLicenseAsync(string licenseKey, string machineId)
        {
            var result = await ExecuteScalarAsync<int>(
                "sp_ValidateLicenseOnly",
                ("@LicenseKey", licenseKey),
                ("@MachineId", machineId));

            return result == 1;
        }

        public async Task<LicenseValidationResult> ValidateAndActivateLicenseAsync(string licenseKey, string machineId)
        {
            var result = await ExecuteScalarAsync<int>(
                "sp_ValidateLicense",
                ("@LicenseKey", licenseKey),
                ("@MachineId", machineId));

            return new LicenseValidationResult
            {
                IsValid = result == 1,
                Status = MapValidationStatus(result),
                Message = GetValidationMessage(result)
            };
        }

        public async Task<int> GetActivationCountAsync(int licenseId)
        {
            return await ExecuteScalarAsync<int>(
                "sp_GetLicenseActivationCount",
                ("@LicenseId", licenseId));
        }

        // Métodos privados de mapeo y utilidad
        private License MapToLicense(SqlDataReader reader)
        {
            var license = new License(
                reader.GetString(reader.GetOrdinal("LicenseKey")),
                reader.GetInt32(reader.GetOrdinal("CustomerId")),
                reader.GetInt32(reader.GetOrdinal("ProductId")),
                reader.GetInt32(reader.GetOrdinal("LicenseTypeId")),
                reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                reader.GetInt32(reader.GetOrdinal("MaxActivations"))
            );

            license.SetLicenseId(reader.GetInt32(reader.GetOrdinal("LicenseId")));
            license.SetCurrentActivations(reader.GetInt32(reader.GetOrdinal("CurrentActivations")));
            license.SetStatus((License.LicenseStatus)Enum.Parse(
                typeof(License.LicenseStatus),
                reader.GetString(reader.GetOrdinal("Status"))));
            license.SetNotes(reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")));

            return license;
        }

        private LicenseValidationResult.ValidationStatus MapValidationStatus(int result)
        {
            switch (result)
            {
                case 1: return LicenseValidationResult.ValidationStatus.Valid;
                case -1: return LicenseValidationResult.ValidationStatus.Invalid;
                case -2: return LicenseValidationResult.ValidationStatus.Expired;
                case -3: return LicenseValidationResult.ValidationStatus.MaxActivationsReached;
                default: return LicenseValidationResult.ValidationStatus.Invalid;
            }
        }

        private string GetValidationMessage(int result)
        {
            switch (result)
            {
                case 1: return "Licencia válida";
                case -1: return "Licencia inválida o inactiva";
                case -2: return "Licencia expirada";
                case -3: return "Máximo de activaciones alcanzado";
                default: return "Error desconocido en la validación";
            }
        }
    }
}