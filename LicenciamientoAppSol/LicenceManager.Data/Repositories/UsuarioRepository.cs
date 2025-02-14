using LicenceManager.Data.Helpers;
using LicenceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceManager.Data.Repositories
{
    public class UsuarioRepository
    {
        private readonly DatabaseHelper _dbHelper;
        public UsuarioRepository(DatabaseHelper dbHelper) 
        {
            _dbHelper = dbHelper;
        }
        public Usuario GetByUsername(string username)
        {
            var query = "SELECT UsuarioID, NombreUsuario, CorreoElectronico, ContrasenaHash, Sal, RolID, EmpresaID, Activo FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@NombreUsuario", username)
            };

            using (var reader = _dbHelper.ExecuteReader(query, parameters))
            {
                if (reader.Read())
                {
                    return new Usuario
                    {
                        UsuarioID = reader.GetInt32(0),
                        NombreUsuario = reader.GetString(1),
                        CorreoElectronico = reader.GetString(2),
                        ContrasenaHash = reader.GetString(3),
                        Sal = reader.GetString(4),
                        RolID = reader.GetInt32(5),
                        EmpresaID = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                        Activo = reader.GetBoolean(7)
                    };
                }
            }
            return null;
        }
    }
    
}
