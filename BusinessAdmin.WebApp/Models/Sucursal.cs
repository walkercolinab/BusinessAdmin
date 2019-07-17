using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class Sucursal
    {
        public Sucursal()
        {
            this.EsActivo = true;
        }
        public long SucursalID { get; set; }

        [Required(ErrorMessage = "Nombres es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Direccion es requerido.")]
        public string Direccion { get; set; }

        [DisplayName("Telefono de casa")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Telefono de domicilio debe ser un numero valido.")]
        public int? TelefonoFijo { get; set; }

        [DisplayName("Telefono movil")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Telefono movil debe ser un numero valido.")]
        public int? TelefonoMovil { get; set; }

        [DisplayName("Activo")]
        public bool EsActivo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; }
        public virtual ICollection<Peluquero> Peluqueros { get; set; }
    }
}