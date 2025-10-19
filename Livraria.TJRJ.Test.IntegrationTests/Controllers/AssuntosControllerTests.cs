using System.Net;
using System.Net.Http.Json;
using Livraria.TJRJ.Test.FuncionalTest.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using static Livraria.TJRJ.Test.FuncionalTest.Infrastructure.TestDataSeeder.TestIds;

namespace Livraria.TJRJ.Test.FuncionalTest.Controllers;

public class AssuntosControllerTests : BaseIntegrationTest
{
    public AssuntosControllerTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Post_DeveRetornar201Created_QuandoAssuntoForCriadoComSucesso()
    {
        // Arrange
        var assunto = new
        {
            Descricao = "Testes"
        };

        // Act
        var response = await Client.PostAsync("/api/assuntos", CreateJsonContent(assunto));

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task Get_DeveRetornar200OK_QuandoBuscarTodosAssuntos()
    {
        // Act
        var response = await Client.GetAsync("/api/assuntos");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_DeveRetornar200OK_QuandoAssuntoExistir()
    {
        // Arrange
        var assuntoId = Assunto1Id; // Programação

        // Act
        var response = await Client.GetAsync($"/api/assuntos/{assuntoId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_DeveRetornar404NotFound_QuandoAssuntoNaoExistir()
    {
        // Arrange
        var assuntoId = AssuntoInexistenteId;

        // Act
        var response = await Client.GetAsync($"/api/assuntos/{assuntoId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Put_DeveRetornar204NoContent_QuandoAssuntoForAtualizadoComSucesso()
    {
        // Arrange
        var assuntoId = Assunto2Id; // Arquitetura
        var assuntoAtualizado = new
        {
            Descricao = "Arquitetura SW"
        };

        // Act
        var response = await Client.PutAsync($"/api/assuntos/{assuntoId}", CreateJsonContent(assuntoAtualizado));

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Put_DeveRetornar404NotFound_QuandoAssuntoNaoExistir()
    {
        // Arrange
        var assuntoId = AssuntoInexistenteId;
        var assuntoAtualizado = new
        {
            Descricao = "Assunto Inexistente"
        };

        // Act
        var response = await Client.PutAsync($"/api/assuntos/{assuntoId}", CreateJsonContent(assuntoAtualizado));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_DeveRetornar204NoContent_QuandoAssuntoForRemovidoComSucesso()
    {
        // Arrange
        var assunto = new
        {
            Descricao = "Testes Exclusao"
        };

        var responsePost = await Client.PostAsync("/api/assuntos", CreateJsonContent(assunto));
        Assert.Equal(HttpStatusCode.Created, responsePost.StatusCode);
        var createdResult = await responsePost.Content.ReadFromJsonAsync<int>();

        // Act
        var response = await Client.DeleteAsync($"/api/assuntos/{createdResult}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_DeveRetornar404NotFound_QuandoAssuntoNaoExistir()
    {
        // Arrange
        var assuntoId = AssuntoInexistenteId;

        // Act
        var response = await Client.DeleteAsync($"/api/assuntos/{assuntoId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_DeveRetornar400BadRequest_QuandoDescricaoForVazia()
    {
        // Arrange
        var assuntoInvalido = new
        {
            Descricao = ""
        };

        // Act
        var response = await Client.PostAsync("/api/assuntos", CreateJsonContent(assuntoInvalido));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
