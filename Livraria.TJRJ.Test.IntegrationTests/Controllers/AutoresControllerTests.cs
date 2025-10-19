using System.Net;
using System.Net.Http.Json;
using Livraria.TJRJ.Test.FuncionalTest.Infrastructure;
using static Livraria.TJRJ.Test.FuncionalTest.Infrastructure.TestDataSeeder.TestIds;

namespace Livraria.TJRJ.Test.FuncionalTest.Controllers;

public class AutoresControllerTests : BaseIntegrationTest
{
    public AutoresControllerTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Post_DeveRetornar201Created_QuandoAutorForCriadoComSucesso()
    {
        // Arrange
        var autor = new
        {
            Nome = "Kent Beck"
        };

        // Act
        var response = await Client.PostAsync("/api/autores", CreateJsonContent(autor));

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task Get_DeveRetornar200OK_QuandoBuscarTodosAutores()
    {
        // Act
        var response = await Client.GetAsync("/api/autores");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_DeveRetornar200OK_QuandoAutorExistir()
    {
        // Arrange
        var autorId = Autor1Id; // Robert C. Martin

        // Act
        var response = await Client.GetAsync($"/api/autores/{autorId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_DeveRetornar404NotFound_QuandoAutorNaoExistir()
    {
        // Arrange
        var autorId = AutorInexistenteId;

        // Act
        var response = await Client.GetAsync($"/api/autores/{autorId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Put_DeveRetornar204NoContent_QuandoAutorForAtualizadoComSucesso()
    {
        // Arrange
        var autorId = Autor2Id; // Martin Fowler
        var autorAtualizado = new
        {
            Nome = "Martin Fowler Updated"
        };

        // Act
        var response = await Client.PutAsync($"/api/autores/{autorId}", CreateJsonContent(autorAtualizado));

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Put_DeveRetornar404NotFound_QuandoAutorNaoExistir()
    {
        // Arrange
        var autorId = AutorInexistenteId;
        var autorAtualizado = new
        {
            Nome = "Autor Inexistente"
        };

        // Act
        var response = await Client.PutAsync($"/api/autores/{autorId}", CreateJsonContent(autorAtualizado));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_DeveRetornar204NoContent_QuandoAutorForRemovidoComSucesso()
    {
        // Arrange
        var autor = new
        {
            Nome = "Ator Novo"
        };

        var responsePost = await Client.PostAsync("/api/autores", CreateJsonContent(autor));
        Assert.Equal(HttpStatusCode.Created, responsePost.StatusCode);
        var createdResult = await responsePost.Content.ReadFromJsonAsync<int>();

        // Act
        var response = await Client.DeleteAsync($"/api/autores/{createdResult}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_DeveRetornar404NotFound_QuandoAutorNaoExistir()
    {
        // Arrange
        var autorId = AutorInexistenteId;

        // Act
        var response = await Client.DeleteAsync($"/api/autores/{autorId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_DeveRetornar400BadRequest_QuandoNomeForVazio()
    {
        // Arrange
        var autorInvalido = new
        {
            Nome = ""
        };

        // Act
        var response = await Client.PostAsync("/api/autores", CreateJsonContent(autorInvalido));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
