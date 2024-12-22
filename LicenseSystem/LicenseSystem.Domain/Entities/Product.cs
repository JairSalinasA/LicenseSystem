using LicenseSystem.Common.Exceptions;
using LicenseSystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Domain.Entities
{
    // LicenseSystem.Domain/Entities/Product.cs
    public class Product
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductCode { get; private set; }
        public string Description { get; private set; }
        public string Version { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public bool IsActive { get; private set; }

        public Product(string productName, string productCode, string version,
            string description = null)
        {
            ValidateProduct(productName, productCode, version);

            ProductName = productName;
            ProductCode = productCode;
            Version = version;
            Description = description;
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
        }

        private void ValidateProduct(string productName, string productCode, string version)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new DomainValidationException("El nombre del producto es requerido.");

            if (string.IsNullOrWhiteSpace(productCode))
                throw new DomainValidationException("El código del producto es requerido.");

            if (string.IsNullOrWhiteSpace(version))
                throw new DomainValidationException("La versión del producto es requerida.");
        }

        public void Update(string productName, string productCode, string version,
            string description)
        {
            ValidateProduct(productName, productCode, version);

            ProductName = productName;
            ProductCode = productCode;
            Version = version;
            Description = description;
            ModifiedDate = DateTime.Now;
        }

        public void Deactivate()
        {
            IsActive = false;
            ModifiedDate = DateTime.Now;
        }
    }
}
