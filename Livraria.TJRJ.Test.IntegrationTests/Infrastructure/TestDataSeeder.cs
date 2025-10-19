using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Enums;
using Livraria.TJRJ.API.Infra.Data;

namespace Livraria.TJRJ.Test.FuncionalTest.Infrastructure;

public static class TestDataSeeder
{
    public static void SeedData(ApplicationDbContext context)
    {
        // Limpa o banco antes de fazer o seed
        context.Livros.RemoveRange(context.Livros);
        context.Autores.RemoveRange(context.Autores);
        context.Assuntos.RemoveRange(context.Assuntos);
        context.SaveChanges();

        // Cria autores
        var autor1 = new Autor("Robert C. Martin");
        var autor2 = new Autor("Martin Fowler");
        var autor3 = new Autor("Eric Evans");

        context.Autores.AddRange(autor1, autor2, autor3);
        context.SaveChanges();

        // Cria assuntos
        var assunto1 = new Assunto("Programação");
        var assunto2 = new Assunto("Arquitetura");
        var assunto3 = new Assunto("Design Patterns");

        context.Assuntos.AddRange(assunto1, assunto2, assunto3);
        context.SaveChanges();

        // Cria livros
        var livro1 = new Livro("Clean Code", "Prentice Hall", 1, "2008");
        livro1.AdicionarAutor(autor1);
        livro1.AdicionarAssunto(assunto1);
        livro1.DefinirPreco(89.90m, FormaDeCompra.Balcao);
        livro1.DefinirPreco(79.90m, FormaDeCompra.Internet);

        var livro2 = new Livro("Refactoring", "Addison-Wesley", 2, "2018");
        livro2.AdicionarAutor(autor2);
        livro2.AdicionarAssunto(assunto1);
        livro2.AdicionarAssunto(assunto3);
        livro2.DefinirPreco(95.00m, FormaDeCompra.Balcao);
        livro2.DefinirPreco(85.00m, FormaDeCompra.Internet);

        var livro3 = new Livro("Domain-Driven Design", "Addison-Wesley", 1, "2003");
        livro3.AdicionarAutor(autor3);
        livro3.AdicionarAssunto(assunto2);
        livro3.DefinirPreco(120.00m, FormaDeCompra.Balcao);
        livro3.DefinirPreco(110.00m, FormaDeCompra.Internet);

        context.Livros.AddRange(livro1, livro2, livro3);
        context.SaveChanges();
    }

    // IDs conhecidos para usar nos testes (após o SaveChanges, os IDs serão 1, 2, 3)
    public static class TestIds
    {
        public const int Autor1Id = 1;  // Robert C. Martin
        public const int Autor2Id = 2;  // Martin Fowler
        public const int Autor3Id = 3;  // Eric Evans

        public const int Assunto1Id = 1;  // Programação
        public const int Assunto2Id = 2;  // Arquitetura
        public const int Assunto3Id = 3;  // Design Patterns

        public const int Livro1Id = 1;  // Clean Code
        public const int Livro2Id = 2;  // Refactoring
        public const int Livro3Id = 3;  // Domain-Driven Design

        public const int LivroInexistenteId = 999;
        public const int AutorInexistenteId = 999;
        public const int AssuntoInexistenteId = 999;
    }
}
