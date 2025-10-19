using System.Net;
using Livraria.TJRJ.Test.FuncionalTest.Infrastructure;

namespace Livraria.TJRJ.Test.FuncionalTest.Controllers;

public class RelatoriosControllerTests : BaseIntegrationTest
{
    public RelatoriosControllerTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_DeveRetornar200OK_QuandoBuscarRelatorioLivrosPorAutor()
    {
        // Act
        var response = await Client.GetAsync("/api/relatorios/livros-por-autor");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_DeveRetornarDadosAgrupados_QuandoBuscarRelatorioLivrosPorAutor()
    {
        // Act
        var response = await Client.GetAsync("/api/relatorios/livros-por-autor");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
        Assert.NotEmpty(content);
    }

    [Fact]
    public async Task Get_DeveRetornar200OK_QuandoBuscarRelatorioComFiltroDeAutor()
    {
        // Arrange
        var autorId = 0;

        // Act
        var response = await Client.GetAsync($"/api/relatorios/livros-por-autor?autorId={autorId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_DeveRetornarFormatoJSON_QuandoSolicitado()
    {
        // Arrange
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        // Act
        var response = await Client.GetAsync("/api/relatorios/livros-por-autor");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }
}
