using BusinessAdmin.WebApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models
{
    public class Usuario
    {
        public Usuario()
        {
            this.EsActivo = true;
            this.EsBloqueado = false;
            this.IntentosFallidos = 0;
        }
        public long UsuarioID { get; set; }
        
        [Required(ErrorMessage = "Nombres es requerido.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellido Paterno es requerido.")]
        [DisplayName("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Telefono de casa")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Telefono de domicilio debe ser un numero valido.")]
        public int? TelefonoFijo { get; set; }

        [DisplayName("Telefono movil")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Telefono movil debe ser un numero valido.")]
        public int? TelefonoMovil { get; set; }

        [DisplayName("Activo")]
        public bool EsActivo { get; set; }

        [Required(ErrorMessage = "Nombre de usuario es requerido.")]
        [MaxLength(25, ErrorMessage = "Nombre de usuario debe ser maximo 25 caracteres")]
        [MinLength(7, ErrorMessage = "Nombre de usuario debe tener al menos 7 caracteres")]
        [DisplayName("Nombre usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Password es requerido.")]
        [MinLength(7, ErrorMessage = "Password de usuario debe tener al menos 7 caracteres")]
        public string Password { get; set; }

        [DisplayName("Confirmar Password")]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password y confirmar password deben ser iguales")]
        public string ConfirmarPassword { get; set; }

        [DisplayName("Bloqueado")]
        [Required]
        public bool EsBloqueado { get; set; }

        public int IntentosFallidos { get; set; }
        
        public virtual ICollection<PermisosUsuario> PermisosUsuario { get; set; }
        public virtual ICollection<CobroServicio> CobroServicios { get; set; }
        public virtual ICollection<CobroProducto> CobroProductos { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }

        private BdContexto db = new BdContexto();
        public Usuario Obtener(int id)
        {
            var usuario = new Usuario();

            try
            {
                //using (var ctx = new TestContext())
                //{
                db.Configuration.LazyLoadingEnabled = false;

                usuario = db.Usuarios.Include("PermisosUsuario")
                                     .Where(x => x.UsuarioID == id).SingleOrDefault();
                //}
            }
            catch (Exception e)
            {
                throw;
            }

            return usuario;
        }
        
    }
}