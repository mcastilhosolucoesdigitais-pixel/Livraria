export interface IRelatorioLivrosPorAutor {
  autorId: number;
  autorNome: string;
  livros: ILivroRelatorio[];
}

export interface ILivroRelatorio {
  autorId: number;
  autorNome: string;
  livroId: number;
  livroTitulo: string;
  livroEditora: string;
  livroEdicao: number;
  livroAnoPublicacao: string;
  assuntos: string;
  precoBalcao: number;
  precoSelfService: number;
  precoInternet: number;
  precoEvento: number;
}
