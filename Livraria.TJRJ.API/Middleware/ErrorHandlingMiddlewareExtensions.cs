namespace Livraria.TJRJ.API.Middleware;

/// <summary>
/// Extensões para configuração do middleware de tratamento de erros
/// </summary>
public static class ErrorHandlingMiddlewareExtensions
{
    /// <summary>
    /// Adiciona o middleware de tratamento de erros global ao pipeline de requisições
    /// </summary>
    /// <param name="app">A instância do IApplicationBuilder</param>
    /// <returns>A mesma instância do IApplicationBuilder para encadeamento de chamadas</returns>
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
