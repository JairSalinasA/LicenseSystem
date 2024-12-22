using LicenseSystem.Common.Exceptions;
using LicenseSystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Domain.Entities
{
    public class License
    {
        public int LicenseId { get; private set; }
        public string LicenseKey { get; private set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }
        public int LicenseTypeId { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public int MaxActivations { get; private set; }
        public int CurrentActivations { get; private set; }
        public LicenseStatus Status { get; private set; }
        public string Notes { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }

        // Método para establecer la clave
        public void SetLicenseKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new DomainValidationException("La clave de licencia no puede estar vacía.");

            LicenseKey = key;
        }

        // Value Object para el estado de la licencia
        public enum LicenseStatus
        {
            Active,
            Expired,
            Suspended,
            Revoked
        }

        // Constructor para nueva licencia
        public License(string licenseKey, int customerId, int productId,
            int licenseTypeId, DateTime expirationDate, int maxActivations)
        {
            if (string.IsNullOrWhiteSpace(licenseKey))
                throw new DomainValidationException("La clave de licencia es requerida.");

            if (expirationDate <= DateTime.Now)
                throw new DomainValidationException("La fecha de expiración debe ser futura.");

            if (maxActivations <= 0)
                throw new DomainValidationException("El número máximo de activaciones debe ser mayor a cero.");

            LicenseKey = licenseKey;
            CustomerId = customerId;
            ProductId = productId;
            LicenseTypeId = licenseTypeId;
            ExpirationDate = expirationDate;
            MaxActivations = maxActivations;
            IssueDate = DateTime.Now;
            Status = LicenseStatus.Active;
            CurrentActivations = 0;
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        // Métodos de dominio
        public bool CanActivate()
        {
            return Status == LicenseStatus.Active &&
                   CurrentActivations < MaxActivations &&
                   DateTime.Now <= ExpirationDate;
        }

        public void IncrementActivations()
        {
            if (!CanActivate())
                throw new DomainValidationException("La licencia no puede ser activada.");

            CurrentActivations++;
            ModifiedDate = DateTime.Now;
        }

        public void Suspend(string notes)
        {
            Status = LicenseStatus.Suspended;
            Notes = notes;
            ModifiedDate = DateTime.Now;
        }

        public void Revoke(string notes)
        {
            Status = LicenseStatus.Revoked;
            Notes = notes;
            ModifiedDate = DateTime.Now;
        }

        public void CheckExpiration()
        {
            if (DateTime.Now > ExpirationDate && Status == LicenseStatus.Active)
            {
                Status = LicenseStatus.Expired;
                ModifiedDate = DateTime.Now;
            }
        }


        public void SetLicenseId(int licenseId)
        {
            if (licenseId <= 0)
                throw new DomainValidationException("El ID de la licencia debe ser positivo.");

            LicenseId = licenseId;
        }

        public void SetCurrentActivations(int activations)
        {
            if (activations < 0)
                throw new DomainValidationException("El número de activaciones no puede ser negativo.");

            CurrentActivations = activations;
        }

        public void SetStatus(LicenseStatus status)
        {
            Status = status;
        }

        public void SetNotes(string notes)
        {
            Notes = notes;
        }

    }
}