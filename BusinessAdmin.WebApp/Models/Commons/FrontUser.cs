using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.Models.Commons
{
    public class FrontUser
    {
        public static bool TienePermiso(ePermiso valor)
        {
            int valorInt = (int)valor;
            var usuario = FrontUser.Get();
            if (usuario != null)
            {
                return usuario.PermisosUsuario.Where(x => x.PermisoID == valorInt)
                                   .Any();
            }
            else
                return false;
        }

        public static Usuario Get()
        {
            return new Usuario().Obtener(SessionHelper.GetUser());
        }
    }
}