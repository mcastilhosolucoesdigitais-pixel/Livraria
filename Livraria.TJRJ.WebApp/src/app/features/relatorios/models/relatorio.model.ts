export interface IRelatorioLivrosPorAutor {
  autorCodigo: number;
  autorNome: string;
  livros: ILivroRelatorio[];
}

export interface ILivroRelatorio {
  codigoLivro: number;
  titulo: string;
  editora: string;
  edicao: number;
  anoPublicacao: string;
  assuntos: string[];
  valores: IValorRelatorio[];
}

export interface IValorRelatorio {
  formaDeCompra: string;
  valor: number;
}
