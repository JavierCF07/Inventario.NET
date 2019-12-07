using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventarioAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventarioAPI.Contexts
{
    public class InventarioDBContext : DbContext
    {
        #region Tablas
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TipoEmpaque> TipoEmpaques { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<TelefonoCliente> TelefonoClientes { get; set; }
        public DbSet<EmailCliente> EmailClientes { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<EmailProveedor> EmailProveedores { get; set; }
        public DbSet <TelefonoProveedor> TelefonoProveedores { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        #endregion

        public InventarioDBContext(DbContextOptions<InventarioDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().ToTable("Categoria")
                .HasKey(key => key.codigoCategoria);
            modelBuilder.Entity<TipoEmpaque>().ToTable("TipoEmpaque")
                .HasKey(key => key.codigoEmpaque);
            modelBuilder.Entity<Producto>().ToTable("Productos")
                .HasKey(key => key.codigoProducto);
            modelBuilder.Entity<Clientes>().ToTable("Clientes")
                .HasKey(key => key.nit);
            modelBuilder.Entity<EmailCliente>().ToTable("EmailCliente")
                .HasKey(key => key.codigoEmail);
            modelBuilder.Entity<TelefonoCliente>().ToTable("TelefonoCliente")
                .HasKey(key => key.codigoTelefono);
            modelBuilder.Entity<Proveedores>().ToTable("Proveedores")
                .HasKey(key => key.codigoProveedor);
            modelBuilder.Entity<EmailProveedor>().ToTable("EmailProveedor")
                .HasKey(key => key.codigoEmail);
            modelBuilder.Entity<TelefonoProveedor>().ToTable("TelefonoProveedor")
                .HasKey(key => key.codigoTelefono);
            modelBuilder.Entity<Compras>().ToTable("Compras")
                .HasKey(key => key.idCompra);
            modelBuilder.Entity<DetalleCompra>().ToTable("DetalleCompra")
                .HasKey(key => key.idDetalle);
            modelBuilder.Entity<Factura>().ToTable("Factura")
                .HasKey(key => key.numeroFactura);
            modelBuilder.Entity<DetalleFactura>().ToTable("DetalleFactura")
                .HasKey(key => key.codigoDetalle);
            modelBuilder.Entity<Inventario>().ToTable("Inventario")
                .HasKey(key => key.codigoInventario);
            base.OnModelCreating(modelBuilder);
        }


    }
}
