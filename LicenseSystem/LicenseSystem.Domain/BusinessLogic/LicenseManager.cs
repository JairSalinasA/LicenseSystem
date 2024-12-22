
using LicenseSystem.Domain.Entities;
using LicenseSystem.Domain.Exceptions;
using LicenseSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Domain.BusinessLogic
{
    public class LicenseManager
    {
        private readonly ILicenseDAO _licenseDAO;
        private readonly ICustomerDAO _customerDAO;
        private readonly IProductDAO _productDAO;
        private readonly ValidationManager _validationManager;

        public LicenseManager(
            ILicenseDAO licenseDAO,
            ICustomerDAO customerDAO,
            IProductDAO productDAO,
            ValidationManager validationManager)
        {
            _licenseDAO = licenseDAO ?? throw new ArgumentNullException(nameof(licenseDAO));
            _customerDAO = customerDAO ?? throw new ArgumentNullException(nameof(customerDAO));
            _productDAO = productDAO ?? throw new ArgumentNullException(nameof(productDAO));
            _validationManager = validationManager ?? throw new ArgumentNullException(nameof(validationManager));
        }

        public async Task<int> CreateLicenseAsync(License license)
        {
            // Validaciones de negocio
            var customer = await _customerDAO.GetByIdAsync(license.CustomerId);
            if (customer == null || !customer.IsActive)
                throw new BusinessException("El cliente no existe o está inactivo.");

            var product = await _productDAO.GetByIdAsync(license.ProductId);
            if (product == null || !product.IsActive)
                throw new BusinessException("El producto no existe o está inactivo.");

            // Generar clave de licencia única
            license.SetLicenseKey(_validationManager.GenerateLicenseKey());

            return await _licenseDAO.CreateAsync(license);
        }

        public async Task<LicenseValidationResult> ValidateAndActivateLicenseAsync(
            string licenseKey, string machineId)
        {
            return await _licenseDAO.ValidateAndActivateLicenseAsync(licenseKey, machineId);
        }

        public async Task RevokeLicenseAsync(int licenseId, string reason)
        {
            var license = await _licenseDAO.GetByIdAsync(licenseId);
            if (license == null)
                throw new BusinessException("Licencia no encontrada.");

            license.Revoke(reason);
            await _licenseDAO.UpdateAsync(license);
        }

        public async Task<IEnumerable<License>> GetCustomerLicensesAsync(int customerId)
        {
            var customer = await _customerDAO.GetByIdAsync(customerId);
            if (customer == null)
                throw new BusinessException("Cliente no encontrado.");

            return (IEnumerable<License>)await _licenseDAO.GetByCustomerIdAsync(customerId);
        }
    }
}
