using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Livraria.TJRJ.API.Infra.Data;

/// <summary>
/// Classe responsável por inicializar objetos do banco de dados (views, stored procedures, etc.)
/// </summary>
public static class DatabaseInitializer
{
    /// <summary>
    /// Garante que a view VIEW_LISTA_PRECOS existe no banco de dados
    /// Se não existir, cria a view usando o script
    /// </summary>
    public static async Task EnsureViewsCreatedAsync(this ApplicationDbContext context)
    {
        const string viewName = "VIEW_LISTA_PRECOS";

        // Verifica se a view existe
        var viewExists = await CheckIfViewExistsAsync(context, viewName);

        if (!viewExists)
        {
            await CreateViewListaPrecosAsync(context);
        }
    }

    private static async Task<bool> CheckIfViewExistsAsync(ApplicationDbContext context, string viewName)
    {
        const string checkViewSql = @"
            SELECT COUNT(*)
            FROM INFORMATION_SCHEMA.VIEWS
            WHERE TABLE_NAME = @ViewName AND TABLE_SCHEMA = 'dbo'";

        await using var command = context.Database.GetDbConnection().CreateCommand();
        command.CommandText = checkViewSql;

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@ViewName";
        parameter.Value = viewName;
        command.Parameters.Add(parameter);

        await context.Database.OpenConnectionAsync();
        try
        {
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }
        finally
        {
            await context.Database.CloseConnectionAsync();
        }
    }

    private static async Task CreateViewListaPrecosAsync(ApplicationDbContext context)
    {
        const string createViewSql = @"
            CREATE VIEW [dbo].[VIEW_LISTA_PRECOS]
            AS
            WITH Precos AS (
                SELECT * FROM Livro_Precos
            ),
            Assuntos AS (
                SELECT a.Id, a.Descricao, l.LivroId
                FROM dbo.Assuntos a
                INNER JOIN Livro_Assunto AS l ON a.Id = l.AssuntoId
            ),
            AssuntosAgregados AS (
                SELECT
                    LivroId,
                    STRING_AGG(Descricao, ', ') AS AssuntosLista
                FROM Assuntos
                GROUP BY LivroId
            ),
            Livros AS (
                SELECT
                    a.Id AS AutorId,
                    a.Nome AS AutorNome,
                    l.Id AS LivroId,
                    l.Titulo AS LivroTitulo,
                    l.Editora AS LivroEditora,
                    l.Edicao AS LivroEdicao,
                    l.AnoPublicacao AS LivroAnoPublicacao
                FROM dbo.Livros l
                INNER JOIN Livro_Autor la ON l.Id = la.LivroId
                INNER JOIN Autores a ON a.Id = la.AutorId
            )
            SELECT
                ll.*,
                aa.AssuntosLista AS Assuntos,
                (SELECT p.Valor FROM Precos p WHERE p.LivroId = ll.LivroId AND p.FormaDeCompra = 'Balcao') AS PrecoBalcao,
                (SELECT p.Valor FROM Precos p WHERE p.LivroId = ll.LivroId AND p.FormaDeCompra = 'SelfService') AS PrecoSelfService,
                (SELECT p.Valor FROM Precos p WHERE p.LivroId = ll.LivroId AND p.FormaDeCompra = 'Internet') AS PrecoInternet,
                (SELECT p.Valor FROM Precos p WHERE p.LivroId = ll.LivroId AND p.FormaDeCompra = 'Evento') AS PrecoEvento
            FROM Livros ll
            LEFT JOIN AssuntosAgregados aa ON ll.LivroId = aa.LivroId";

        await context.Database.ExecuteSqlRawAsync(createViewSql);
    }
}
