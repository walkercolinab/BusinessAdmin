using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class Peluquero
    {
        public long PeluqueroID { get; set; }

        [Required(ErrorMessage = "Nombres es requerido.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellido Paterno es requerido.")]
        [DisplayName("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Cedula de identidad")]
        [Required(ErrorMessage = "Cedula de identidad es requerido.")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Cedula de identidad debe ser un numero valido.")]
        public int CedulaIdentidad { get; set; }

        [DisplayName("Telefono de casa")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Telefono de domicilio debe ser un numero valido.")]
        public int? TelefonoFijo { get; set; }

        [DisplayName("Telefono movil")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Telefono movil debe ser un numero valido.")]
        public int? TelefonoMovil { get; set; }

        [Required(ErrorMessage = "Fecha de ingreso es requerido.")]
        [DisplayName("Fecha de ingreso")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }

        [DisplayName("Modo de contrato")]
        public string ModoContrado { get; set; }

        [DisplayName("Porcentaje por corte")]
        public int PorcentajeCorte { get; set; }

        [DisplayName("Porcentaje por ventas")]
        public int PorcentajeVentas { get; set; }

        [DisplayName("Porcentaje semanal")]
        public int PorcentajeSemanal { get; set; }

        [DisplayName("Activo")]
        public bool EsActivo { get; set; }

        [Required(ErrorMessage = "Sucursal es requerido.")]
        public long? SucursalID { get; set; }
        [ForeignKey("SucursalID")]
        public virtual Sucursal Sucursal { get; set; }

        public virtual ICollection<CobroProducto> CobroProductos { get; set; }
        public virtual ICollection<CobroServicio> CobroServicios { get; set; }
       
    }
}