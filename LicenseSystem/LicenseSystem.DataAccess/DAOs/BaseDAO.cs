using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseSystem.DataAccess.Connection;
using LicenseSystem.DataAccess.Helpers;
using LicenseSystem.Common.Exceptions;
using Microsoft.Extensions.Logging;

// LicenseSystem.DataAccess/DAOs/BaseDAO.cs
namespace LicenseSystem.DataAccess.DAOs
{
    // LicenseSystem.DataAccess/DAOs/BaseDAO.cs
    namespace LicenseSystem.DataAccess.DAOs
    {
        public abstract class BaseDAO
        {
            protected readonly DatabaseConnection _dbConnection;
            private readonly ILogger _logger;

            protected BaseDAO(DatabaseConnection dbConnection, ILogger logger)
            {
                _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            protected async Task<T> ExecuteReaderAsync<T>(string storedProcedure,
                Func<SqlDataReader, T> mapper, params (string name, object value)[] parameters)
            {
                try
                {
                    var connection = await _dbConnection.CreateAsync();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storedProcedure;
                    command.AddParameters(parameters);

                    var reader = await command.ExecuteReaderAsync();
                    return await reader.ReadAsync() ? mapper(reader) : default;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error ejecutando SP: {storedProcedure}");
                    throw new DatabaseException($"Error al ejecutar {storedProcedure}", ex);
                }
            }

            protected async Task<IEnumerable<T>> ExecuteReaderListAsync<T>(string storedProcedure,
                Func<SqlDataReader, T> mapper, params (string name, object value)[] parameters)
            {
                try
                {
                    var connection = await _dbConnection.CreateAsync();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storedProcedure;
                    command.AddParameters(parameters);

                    var results = new List<T>();
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        results.Add(mapper(reader));
                    }
                    return results;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error ejecutando SP: {storedProcedure}");
                    throw new DatabaseException($"Error al ejecutar {storedProcedure}", ex);
                }
            }

            protected async Task<int> ExecuteNonQueryAsync(string storedProcedure,
                params (string name, object value)[] parameters)
            {
                try
                {
                    var connection = await _dbConnection.CreateAsync();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storedProcedure;
                    command.AddParameters(parameters);

                    return await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error ejecutando SP: {storedProcedure}");
                    throw new DatabaseException($"Error al ejecutar {storedProcedure}", ex);
                }
            }

            protected async Task<T> ExecuteScalarAsync<T>(string storedProcedure,
                params (string name, object value)[] parameters)
            {
                try
                {
                    var connection = await _dbConnection.CreateAsync();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storedProcedure;
                    command.AddParameters(parameters);

                    var result = await command.ExecuteScalarAsync();
                    return result == DBNull.Value ? default : (T)result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error ejecutando SP: {storedProcedure}");
                    throw new DatabaseException($"Error al ejecutar {storedProcedure}", ex);
                }
            }
        }
    }
}