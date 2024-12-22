using LicenseSystem.Common.Exceptions;
using LicenseSystem.Domain.Exceptions;
using System;

namespace LicenseSystem.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; private set; }
        public string CompanyName { get; private set; }
        public string ContactName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public bool IsActive { get; private set; }

        public Customer(string companyName, string contactName, string email,
            string phone = null, string address = null)
        {
            ValidateCustomer(companyName, contactName, email);

            CompanyName = companyName;
            ContactName = contactName;
            Email = email;
            Phone = phone;
            Address = address;
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
        }

        private void ValidateCustomer(string companyName, string contactName, string email)
        {
            if (string.IsNullOrWhiteSpace(companyName))
                throw new Exceptions.DomainValidationException("El nombre de la compañía es requerido.");

            if (string.IsNullOrWhiteSpace(contactName))
                throw new DomainValidationException("El nombre del contacto es requerido.");

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                throw new DomainValidationException("El email es inválido.");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void Update(string companyName, string contactName, string email,
            string phone, string address)
        {
            ValidateCustomer(companyName, contactName, email);

            CompanyName = companyName;
            ContactName = contactName;
            Email = email;
            Phone = phone;
            Address = address;
            ModifiedDate = DateTime.Now;
        }

        public void Deactivate()
        {
            IsActive = false;
            ModifiedDate = DateTime.Now;
        }

        public void Activate()
        {
            IsActive = true;
            ModifiedDate = DateTime.Now;
        }
    }
    }