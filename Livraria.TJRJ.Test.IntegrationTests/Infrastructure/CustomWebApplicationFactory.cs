using Livraria.TJRJ.API.Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.TJRJ.Test.FuncionalTest.Infrastructure;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

            // Remove todos os serviços relacionados ao DbContext (SqlServer)
            RemoveAllEntityFrameworkServices(services);

            // Adiciona o DbContext com banco de dados em memória
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });

            // Cria o banco de dados e faz o seed dos dados de teste
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();

            db.Database.EnsureCreated();

            // Seed de dados de teste
            TestDataSeeder.SeedData(db);
        });

        builder.UseEnvironment("Testing");
    }

    private static void RemoveAllEntityFrameworkServices(IServiceCollection services)
    {
        // Lista de tipos que devem ser removidos
        var typesToRemove = new[]
        {
                typeof(DbContextOptions),
                typeof(DbContextOptions<ApplicationDbContext>),
                typeof(ApplicationDbContext)
            };

        foreach (var type in typesToRemove)
        {
            var descriptors = services.Where(d => d.ServiceType == type).ToList();
            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }
        }

        // Remover TODOS os serviços do EF Core para evitar conflitos
        var efCoresToRemove = services.Where(d =>
            d.ServiceType.Namespace != null && (
                d.ServiceType.Namespace.StartsWith("Microsoft.EntityFrameworkCore") ||
                d.ServiceType.Namespace.StartsWith("Npgsql.EntityFrameworkCore") ||
                d.ImplementationType?.Namespace?.StartsWith("Microsoft.EntityFrameworkCore") == true ||
                d.ImplementationType?.Namespace?.StartsWith("Npgsql.EntityFrameworkCore") == true
            )).ToList();

        foreach (var service in efCoresToRemove)
        {
            services.Remove(service);
        }
    }
}
