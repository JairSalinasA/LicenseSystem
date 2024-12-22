using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Common.Constants
{
    public static class StoredProcedures
    {
        // Licencias
        public const string CreateLicense = "sp_CreateLicense";
        public const string ValidateLicense = "sp_ValidateLicense";
        public const string RevokeLicense = "sp_RevokeLicense";
        public const string UpdateLicense = "sp_UpdateLicense";
        public const string GetLicenseDetails = "sp_GetLicenseDetails";

        // Clientes
        public const string CreateCustomer = "sp_CreateCustomer";
        public const string UpdateCustomer = "sp_UpdateCustomer";
        public const string DeleteCustomer = "sp_DeleteCustomer";
        public const string GetCustomerLicenses = "sp_GetCustomerLicenses";

        // Productos
        public const string CreateProduct = "sp_CreateProduct";
        public const string UpdateProduct = "sp_UpdateProduct";
        public const string DeleteProduct = "sp_DeleteProduct";

        public static class Parameters
        {
            // Parámetros de Licencia
            public const string LicenseId = "@LicenseId";
            public const string LicenseKey = "@LicenseKey";
            public const string CustomerId = "@CustomerId";
            public const string ProductId = "@ProductId";
            public const string LicenseTypeId = "@LicenseTypeId";
            public const string IssueDate = "@IssueDate";
            public const string ExpirationDate = "@ExpirationDate";
            public const string MaxActivations = "@MaxActivations";
            public const string CurrentActivations = "@CurrentActivations";
            public const string Status = "@Status";
            public const string Notes = "@Notes";
            public const string MachineId = "@MachineId";

            // Parámetros de Cliente
            public const string CompanyName = "@CompanyName";
            public const string ContactName = "@ContactName";
            public const string Email = "@Email";
            public const string Phone = "@Phone";
            public const string Address = "@Address";

            // Parámetros de Producto
            public const string ProductName = "@ProductName";
            public const string ProductCode = "@ProductCode";
            public const string Description = "@Description";
            public const string Version = "@Version";

            // Parámetros comunes
            public const string IsActive = "@IsActive";
            public const string CreatedDate = "@CreatedDate";
            public const string ModifiedDate = "@ModifiedDate";
            public const string ReturnValue = "@ReturnValue";
        }

        public static class ReturnCodes
        {
            public const int Success = 1;
            public const int Error = 0;
            public const int InvalidLicense = -1;
            public const int ExpiredLicense = -2;
            public const int MaxActivationsReached = -3;
            public const int InvalidCustomer = -4;
            public const int InvalidProduct = -5;
            public const int DuplicateKey = -6;
        }
    }

    public static class ValidationMessages
    {
        public const string InvalidEmail = "El formato del correo electrónico no es válido.";
        public const string InvalidPhone = "El formato del número de teléfono no es válido.";
        public const string RequiredField = "El campo {0} es obligatorio.";
        public const string InvalidLength = "El campo {0} debe tener entre {1} y {2} caracteres.";
        public const string InvalidRange = "El valor de {0} debe estar entre {1} y {2}.";
        public const string InvalidDate = "La fecha {0} no es válida.";
        public const string ExpiredDate = "La fecha {0} ha expirado.";
        public const string DuplicateKey = "Ya existe un registro con el mismo {0}.";
    }
}
