using System.Net;
using Livraria.TJRJ.Test.FuncionalTest.Infrastructure;
using static Livraria.TJRJ.Test.FuncionalTest.Infrastructure.TestDataSeeder.TestIds;

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
            Titulo = "The Pragmatic Programmer",
            Editora = "Addison-Wesley",
            Edicao = 2,
            AnoPublicacao = "2019",
            Autores = new[] { Autor1Id },
            Assuntos = new[] { Assunto1Id },
            ValorInicial = 99.90m,
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
        var livroId = Livro1Id; // Clean Code

        // Act
        var response = await Client.GetAsync($"/api/livros/{livroId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_DeveRetornar404NotFound_QuandoLivroNaoExistir()
    {
        // Arrange
        var livroId = LivroInexistenteId;

        // Act
        var response = await Client.GetAsync($"/api/livros/{livroId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Put_DeveRetornar204NoContent_QuandoLivroForAtualizadoComSucesso()
    {
        // Arrange
        var livroId = Livro1Id;
        var livroAtualizado = new
        {
            Titulo = "Clean Code - Updated",
            Editora = "Prentice Hall",
            Edicao = 2,
            AnoPublicacao = "2008",
            Autores = new[] { Autor1Id },
            Assuntos = new[] { Assunto1Id },
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
        var livroId = LivroInexistenteId;
        var livroAtualizado = new
        {
            Titulo = "Clean Code",
            Editora = "Prentice Hall",
            Edicao = 1,
            AnoPublicacao = "2008",
            Autores = new[] { Autor1Id },
            Assuntos = new[] { Assunto1Id },
            ValorInicial = 89.90m,
            FormaDeCompraInicial = "Balcao"
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
        var livroId = Livro3Id; // Domain-Driven Design

        // Act
        var response = await Client.DeleteAsync($"/api/livros/{livroId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_DeveRetornar404NotFound_QuandoLivroNaoExistir()
    {
        // Arrange
        var livroId = LivroInexistenteId;

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
            Titulo = "", // Título vazio - inválido
            Editora = "",
            Edicao = 0,
            AnoPublicacao = "9999" // Ano futuro - inválido
        };

        // Act
        var response = await Client.PostAsync("/api/livros", CreateJsonContent(livroInvalido));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
