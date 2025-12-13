using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Persistence
{
    public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // 1. Lire la chaîne de connexion
            // Ceci trouve appsettings.json dans votre projet de Démarrage (Facturation.Api)
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json") // Assurez-vous que ce fichier existe
                .Build();

            // 2. Construire les DbContextOptions
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("HexagonalConnection");

            // 🚨 Utilisez la méthode d'extension du provider
            builder.UseSqlServer(connectionString);

            // 3. Retourner une nouvelle instance du DbContext
            return new ApplicationDbContext(builder.Options);
        }

    }
}
