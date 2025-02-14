using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceManager.Data.Helpers
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;
        public DatabaseHelper(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        // Método para ejecutar una consulta que devuelve un DataReader (SELECT)
        public SqlDataReader ExecuteReader(string query, SqlParameter[] parameters = null)
        {
            var connection = GetConnection();
            var command = new SqlCommand(query, connection);

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        // Método para ejecutar una consulta que no devuelve resultados (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteNonQuery();
            }
        }

        // Método para ejecutar una consulta que devuelve un único valor (SELECT COUNT, etc.)
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteScalar();
            }
        }



    }
}
