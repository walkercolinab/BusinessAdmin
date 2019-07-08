using BusinessAdmin.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.DAL
{
    public class DatosInicialesContexto : DropCreateDatabaseIfModelChanges<BdContexto>
    {
        protected override void Seed(BdContexto context)
        {
            #region Usuario Administrador de sistema
            Usuario usuario = new Usuario();
            usuario.UsuarioID = 1;
            usuario.Nombres = "Admin";
            usuario.ApellidoPaterno = "Sistema";
            usuario.ApellidoMaterno = string.Empty;
            usuario.EsActivo = true;
            usuario.TelefonoFijo = 1234567;
            usuario.TelefonoMovil = 1234567;
            usuario.NombreUsuario = "adminSys";
            usuario.Password = Seguridad.Encriptar("Password1");
            usuario.ConfirmarPassword = Seguridad.Encriptar("Password1");
            usuario.EsBloqueado = false;
            usuario.IntentosFallidos = 0;

            try
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            #endregion
        }
    }
}