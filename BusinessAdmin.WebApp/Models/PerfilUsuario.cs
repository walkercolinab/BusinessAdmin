using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class PerfilUsuario
    {
        public PerfilUsuario()
        {
            this.EsActivo = true;
        }

        public int PerfilUsuarioID { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Activo")]
        public bool EsActivo { get; set; }
    }
}