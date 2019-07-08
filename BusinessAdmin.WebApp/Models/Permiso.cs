using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class Permiso
    {
        public int PermisoID { get; set; }
        public string Modulo { get; set; }
        public string Categoria { get; set; }
        public string NombrePermiso { get; set; }
        public bool EsActivo { get; set; }
        public virtual ICollection<PermisosUsuario> PermisosUsuario { get; set; }
    }
}