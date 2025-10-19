using System.Net;
using Livraria.TJRJ.Test.FuncionalTest.Infrastructure;

namespace Livraria.TJRJ.Test.FuncionalTest.Controllers;

public class LivrosControllerTests : BaseIntegrationTest
{
    public LivrosControllerTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Post_DeveRetornar201Created_QuandoLivroForCriadoComSucesso()
    {
        // Arrange
        var livro = new
        {
            Titulo = "Clean Code",
            ISBN = "978-0132350884",
            DataPublicacao = new DateTime(2008, 8, 1),
            AutoresIds = new[] { Guid.NewGuid() },
            AssuntosIds = new[] { Guid.NewGuid() },
            ValorInicial = 89.90m,
            FormaDeCompraInicial = "Balcao"
        };

        // Act
        var response = await Client.PostAsync("/api/livros", CreateJsonContent(livro));

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task Get_DeveRetornar200OK_QuandoBuscarTodosLivros()
    {
        // Act
        var response = await Client.GetAsync("/api/livros");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_DeveRetornar200OK_QuandoLivroExistir()
    {
        // Arrange
        var livroId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"/api/livros/{livroId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_DeveRetornar404NotFound_QuandoLivroNaoExistir()
    {
        // Arrange
        var livroId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"/api/livros/{livroId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Put_DeveRetornar204NoContent_QuandoLivroForAtualizadoComSucesso()
    {
        // Arrange
        var livroId = Guid.NewGuid();
        var livroAtualizado = new
        {
            Titulo = "Clean Code - Updated",
            ISBN = "978-0132350884",
            DataPublicacao = new DateTime(2008, 8, 1),
            AutoresIds = new[] { Guid.NewGuid() },
            AssuntosIds = new[] { Guid.NewGuid() },
            ValorInicial = 99.90m,
            FormaDeCompraInicial = "Internet"
        };

        // Act
        var response = await Client.PutAsync($"/api/livros/{livroId}", CreateJsonContent(livroAtualizado));

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Put_DeveRetornar404NotFound_QuandoLivroNaoExistir()
    {
        // Arrange
        var livroId = Guid.NewGuid();
        var livroAtualizado = new
        {
            Titulo = "Clean Code",
            ISBN = "978-0132350884",
            DataPublicacao = new DateTime(2008, 8, 1)
        };

        // Act
        var response = await Client.PutAsync($"/api/livros/{livroId}", CreateJsonContent(livroAtualizado));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_DeveRetornar204NoContent_QuandoLivroForRemovidoComSucesso()
    {
        // Arrange
        var livroId = Guid.NewGuid();

        // Act
        var response = await Client.DeleteAsync($"/api/livros/{livroId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_DeveRetornar404NotFound_QuandoLivroNaoExistir()
    {
        // Arrange
        var livroId = Guid.NewGuid();

        // Act
        var response = await Client.DeleteAsync($"/api/livros/{livroId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_DeveRetornar400BadRequest_QuandoDadosInvalidos()
    {
        // Arrange
        var livroInvalido = new
        {
            Titulo = "",
            ISBN = "",
            DataPublicacao = DateTime.Now.AddYears(1) // Data futura
        };

        // Act
        var response = await Client.PostAsync("/api/livros", CreateJsonContent(livroInvalido));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
