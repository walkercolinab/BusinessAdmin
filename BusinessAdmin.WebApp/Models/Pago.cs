using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class Pago
    {
        public Pago()
        {
            this.EsActivo = true;
        }
        public long PagoID { get; set; }

        [DisplayName("Monto (Bs.)")]
        public double Monto { get; set; }

        [DisplayName("Salida (Bs.)")]
        public double Salida { get; set; }

        [DisplayName("Cambio (Bs.)")]
        public double Cambio { get; set; }

        [Required(ErrorMessage = "Fecha de pago es requerido.")]
        [DisplayName("Fecha de pago")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaPago { get; set; }

        [DisplayName("Activo")]
        public bool EsActivo { get; set; }

        [Required(ErrorMessage = "Usuario es requerido.")]
        public long UsuarioID { get; set; }

        [Required(ErrorMessage = "Tipo de gasto es requerido.")]
        public long TipoGastoID { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual TipoGasto TipoGasto { get; set; }
    }
}