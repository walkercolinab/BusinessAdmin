using BusinessAdmin.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BusinessAdmin.WebApp.DAL
{
    public class BdContexto : DbContext
    {
        public BdContexto()
            : base("CadenaConexionBaseDeDatos")
        {
        }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<PermisosUsuario> PermisosUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuarios { get; set; }
        public DbSet<PermisosPerfilUsuario> PermisosPerfilUsuarios { get; set; }
        public DbSet<Peluquero> Peluqueros { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<CobroServicio> CobroServicios { get; set; }
        public DbSet<CobroProducto> CobroProductos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<TipoGasto> TipoGastos { get; set; }

    }
}