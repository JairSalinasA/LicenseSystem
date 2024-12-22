using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.DataAccess.Helpers
{
    // LicenseSystem.DataAccess/Helpers/ParameterHelper.cs
    public static class ParameterHelper
    {
        public static SqlParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        public static SqlParameter[] CreateParameters(params (string name, object value)[] parameters)
        {
            return parameters.Select(p => CreateParameter(p.name, p.value)).ToArray();
        }

        public static void AddParameters(this SqlCommand command, params (string name, object value)[] parameters)
        {
            foreach (var (name, value) in parameters)
            {
                command.Parameters.Add(CreateParameter(name, value));
            }
        }
    }
}
