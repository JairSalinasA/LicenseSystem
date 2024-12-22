using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace LicenseSystem.Common.Exceptions
{
    // Exceptions/DatabaseException.cs
    public class DatabaseException : Exception
    {
        public string SqlQuery { get; }
        public Dictionary<string, object> Parameters { get; }
        public string ConnectionString { get; }

        public DatabaseException(string message) : base(message)
        {
        }

        public DatabaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DatabaseException(string message, string sqlQuery,
            Dictionary<string, object> parameters, string connectionString,
            Exception innerException)
            : base(message, innerException)
        {
            SqlQuery = sqlQuery;
            Parameters = parameters;
            ConnectionString = connectionString?.Replace("Password=.*?;", "Password=*****;");
        }
    }
}
