using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class TipoGasto
    {
        public TipoGasto()
        {
            this.EsActivo = true;
        }
        public long TipoGastoID { get; set; }

        public string Nombre { get; set; }

        [DisplayName("Activo")]
        public bool EsActivo { get; set; }

        public virtual ICollection<Pago> Pagos { get; set; }
    }
}