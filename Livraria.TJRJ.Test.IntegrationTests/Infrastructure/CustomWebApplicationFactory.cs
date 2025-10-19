using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Livraria.TJRJ.Test.FuncionalTest.Infrastructure;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // TODO: Configurar banco de dados em memória quando necessário
            // TODO: Adicionar serviços de teste conforme necessário
        });

        builder.UseEnvironment("Testing");
    }
}
