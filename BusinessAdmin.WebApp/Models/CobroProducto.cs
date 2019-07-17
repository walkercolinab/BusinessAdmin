using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class CobroProducto
    {
        public CobroProducto()
        {
            this.EsActivo = true;
        }
        public long CobroProductoID { get; set; }

        [DisplayName("Monto (Bs.)")]
        public double Monto { get; set; }

        [DisplayName("Tipo de pago")]
        public string TipoPago { get; set; }

        [DisplayName("Ingreso (Bs.)")]
        public double Ingreso { get; set; }

        [DisplayName("Cambio (Bs.)")]
        public double Cambio { get; set; }

        [Required(ErrorMessage = "Fecha de cobro es requerido.")]
        [DisplayName("Fecha de cobro")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCobro { get; set; }

        [DisplayName("Activo")]
        public bool EsActivo { get; set; }
        
        [Required(ErrorMessage = "Usuario es requerido.")]
        public long? UsuarioID { get; set; }
        [Required(ErrorMessage = "Producto es requerido.")]
        public long? ProductoID { get; set; }
        [Required(ErrorMessage = "Peluquero es requerido.")]
        public long? PeluqueroID { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Peluquero Peluquero { get; set; }
    }
}