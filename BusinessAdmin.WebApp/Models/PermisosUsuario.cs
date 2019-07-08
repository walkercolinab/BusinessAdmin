using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class PermisosUsuario
    {
        [DisplayName("Identificador")]
        public int PermisosUsuarioID { get; set; }
        public int PermisoID { get; set; }
        public long UsuarioID { get; set; }
        public virtual Permiso Permiso { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}