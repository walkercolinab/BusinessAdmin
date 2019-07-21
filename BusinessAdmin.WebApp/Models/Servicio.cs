using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class Servicio
    {
        public Servicio()
        {
            this.EsActivo = true;
        }
        public long ServicioID { get; set; }
        [Required(ErrorMessage = "Nombre es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Precio base es requerido.")]
        [DisplayName("Precio base (Bs.)")]
        public double PrecioBase { get; set; }

        [DisplayName("Activo")]
        public bool EsActivo { get; set; }

        [Required(ErrorMessage = "Sucursal es requerido.")]
        [DisplayName("Sucursal")]
        public long? SucursalID { get; set; }

        public virtual ICollection<CobroServicio> CobroServicios { get; set; }
        public virtual Sucursal Sucursal { get; set; }
    }
}