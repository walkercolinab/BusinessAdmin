using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class Producto
    {
        public Producto()
        {
            this.EsActivo = true;
            this.Stock = 0;
        }
        public long ProductoID { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Precio de compra (Bs.)")]
        public double PrecioCompra { get; set; }

        [DisplayName("Precio de venta (Bs.)")]
        public double PrecioVenta { get; set; }

        [DisplayName("Numero de Items")]
        public int Stock { get; set; }

        [DisplayName("Activo")]
        public bool EsActivo { get; set; }

        [Required(ErrorMessage = "Sucursal es requerido.")]
        public long? SucursalID { get; set; }

        public virtual ICollection<CobroProducto> CobroProductos { get; set; }
        public virtual Sucursal Sucursal { get; set; }
    }
}