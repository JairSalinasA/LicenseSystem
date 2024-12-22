using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.DataAccess.Connection
{
    // LicenseSystem.DataAccess/Connection/ConnectionString.cs
    namespace LicenseSystem.DataAccess.Connection
    {
        public class ConnectionString
        {
            private readonly string _connectionString;

            public ConnectionString(string connectionString)
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new ArgumentNullException(nameof(connectionString));

                _connectionString = connectionString;
            }

            public string Value => _connectionString;

            public static ConnectionString FromConfiguration(IConfiguration configuration)
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new ConnectionString(connectionString);
            }
        }
    }
}