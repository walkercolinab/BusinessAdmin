using BusinessAdmin.WebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Nombre de usuario")]
        public string NombreUsuario { get; set; }

        [Required]
        public string Password { get; set; }

        public long SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}