
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Entity;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configurer les noms de tables sans le suffixe "Entity"
            modelBuilder.Entity<ProductEntity>().ToTable("Product");
            modelBuilder.Entity<OrderItemEntity>().ToTable("OrderItem"); // <-- DÉCOMMENTÉ/CORRIGÉ

            // 2. Configurer la relation Product <-> OrderItem (Votre configuration existante)
            modelBuilder.Entity<ProductEntity>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductEntityId);

            // 3. Configurer la relation Order <-> OrderItem (NOUVELLE CONF.)
            modelBuilder.Entity<OrderEntity>()
                .HasMany(o => o.Items) // Assumant que 'Items' est la collection dans OrderEntity
                .WithOne(oi => oi.Order) // Navigation prop dans OrderItemEntity
                .HasForeignKey(oi => oi.OrderId); // Clé étrangère dans OrderItemEntity
        }
    }
}