using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceManager.Common.Helpers
{
    public static class AppConfig
    {
        public static string ConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
                if (connectionString == null)
                {
                    throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection' en el archivo de configuración.");
                }
                Console.WriteLine("Cadena de conexión leída: " + connectionString.ConnectionString);
                return connectionString.ConnectionString;
            }
        }
    }
}