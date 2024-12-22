using LicenseSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Domain.Interfaces
{
    // LicenseSystem.Domain/Interfaces/IProductDAO.cs
    public interface IProductDAO : IBaseDAO<Product>
    {
        Task<Product> GetByCodeAsync(string productCode);
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<bool> ProductCodeExistsAsync(string productCode);
    }
}
