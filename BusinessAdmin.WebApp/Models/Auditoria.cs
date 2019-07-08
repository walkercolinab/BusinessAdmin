using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class Auditoria
    {
        public Auditoria()
        {

        }

        public DateTime Fecha { get; set; }
        public long UsuarioID { get; set; }
        public string Tabla { get; set; }
        public string Columna { get; set; }
        public long IdFila { get; set; }
        public string ValorAnterior { get; set; }
        public string NuevoValor { get; set; }
    }
}