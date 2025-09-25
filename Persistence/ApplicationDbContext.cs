
using Microsoft.EntityFrameworkCore;
using Persistence.Entity;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration pour l'Entité Client
            modelBuilder.Entity<ClientEntity>(entity =>
            {
                // Définit Id comme Clé Primaire (int)
                entity.HasKey(c => c.Id);
                // Exemple : Le nom ne peut pas être null
                entity.Property(c => c.Nom).IsRequired();

                // Relation One-to-Many avec Invoice
                entity.HasMany(c => c.Invoices)
                      .WithOne(i => i.Client)
                      .HasForeignKey(i => i.ClientId);
            });

            // Configuration pour l'Entité Invoice
            modelBuilder.Entity<InvoiceEntity>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Numero).IsRequired();

                // Configuration de l'Objet Valeur/Entité Dépendante InvoiceLineEntity
                entity.OwnsMany(i => i.InvoiceLines, line =>
                {
                    // Définit la clé composite : InvoiceId (FK) et Id (numéro de ligne)
                    line.HasKey(l => new { l.InvoiceId, l.Id });

                    line.ToTable("InvoiceLines");

                    // Mappage des propriétés de la ligne
                    line.Property(l => l.Description).IsRequired();
                });
            });

        }
    }
}