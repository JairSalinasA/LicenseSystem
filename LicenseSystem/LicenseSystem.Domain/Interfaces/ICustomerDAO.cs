using LicenseSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Domain.Interfaces
{
    // LicenseSystem.Domain/Interfaces/ICustomerDAO.cs
    public interface ICustomerDAO : IBaseDAO<Customer>
    {
        Task<Customer> GetByEmailAsync(string email);
        Task<IEnumerable<Customer>> GetActiveCustomersAsync();
        Task<bool> EmailExistsAsync(string email);
    }
}
