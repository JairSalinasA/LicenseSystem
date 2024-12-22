using LicenseSystem.Common.Exceptions;
using LicenseSystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Domain.Entities
{
    // LicenseSystem.Domain/Entities/LicenseType.cs
    public class LicenseType
    {
        public int LicenseTypeId { get; private set; }
        public string TypeName { get; private set; }
        public string Description { get; private set; }
        public int DurationDays { get; private set; }
        public int? MaxUsers { get; private set; }
        public bool IsActive { get; private set; }

        public LicenseType(string typeName, int durationDays, int? maxUsers = null,
            string description = null)
        {
            ValidateLicenseType(typeName, durationDays);

            TypeName = typeName;
            DurationDays = durationDays;
            MaxUsers = maxUsers;
            Description = description;
            IsActive = true;
        }

        private void ValidateLicenseType(string typeName, int durationDays)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new DomainValidationException("El nombre del tipo de licencia es requerido.");

            if (durationDays <= 0)
                throw new DomainValidationException("La duración debe ser mayor a cero días.");
        }

        public void Update(string typeName, int durationDays, int? maxUsers,
            string description)
        {
            ValidateLicenseType(typeName, durationDays);

            TypeName = typeName;
            DurationDays = durationDays;
            MaxUsers = maxUsers;
            Description = description;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
