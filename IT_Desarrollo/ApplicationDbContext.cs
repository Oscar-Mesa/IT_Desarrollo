using GusticosWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GusticosWebAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId);


            modelBuilder.Entity<VentaProducto>()
                .HasOne(vp => vp.Venta)
                .WithMany(v => v.VentaProductos)
                .HasForeignKey(vp => vp.VentaId);

            modelBuilder.Entity<VentaProducto>()
                .HasOne(vp => vp.Producto)
                .WithMany(p => p.VentaProductos)
                .HasForeignKey(vp => vp.ProductoId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Categoria> tbl_categorias => Set<Categoria>();
        public DbSet<Producto> tbl_productos => Set<Producto>();
        public DbSet<Venta> tbl_ventas => Set<Venta>();
        public DbSet<VentaProducto> tbl_ventaProductos => Set<VentaProducto>();

    }
}
