using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceManager.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string ContrasenaHash { get; set; }
        public string Sal { get; set; }
        public int RolID { get; set; }
        public int? EmpresaID { get; set; }
        public bool Activo { get; set; } 


    }
}
