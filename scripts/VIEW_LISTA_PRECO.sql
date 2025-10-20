USE [LivrariaTJRJ]
GO

/****** Object:  View [dbo].[VIEW_LISTA_PRECOS]    Script Date: 19/10/2025 21:41:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


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
LEFT JOIN AssuntosAgregados aa ON ll.LivroId = aa.LivroId
GO


