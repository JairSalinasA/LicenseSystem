using LicenseSystem.Common.Exceptions;
using LicenseSystem.DataAccess.Connection.LicenseSystem.DataAccess.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.DataAccess.Connection
{
    // LicenseSystem.DataAccess/Connection/DatabaseConnection.cs
    public class DatabaseConnection
    {
        private readonly ConnectionString _connectionString;

        public DatabaseConnection(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Create()
        {
            var connection = new SqlConnection(_connectionString.Value);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                connection.Dispose();
                throw new DatabaseException("Error al conectar con la base de datos", ex);
            }
        }

        public async Task<SqlConnection> CreateAsync()
        {
            var connection = new SqlConnection(_connectionString.Value);
            try
            {
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                connection.Dispose(); // Usamos Dispose en lugar de DisposeAsync
                throw new DatabaseException("Error al conectar con la base de datos", ex);
            }
        }


    }
}
