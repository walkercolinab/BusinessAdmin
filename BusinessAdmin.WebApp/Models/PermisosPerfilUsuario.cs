using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class PermisosPerfilUsuario
    {
        [DisplayName("Identificador")]
        public int PermisosPerfilUsuarioID { get; set; }
        public int? PermisoID { get; set; }
        public long? PerfilUsuarioID { get; set; }
        public virtual Permiso Permiso { get; set; }
        public virtual PerfilUsuario PerfilUsuario { get; set; }
    }
}