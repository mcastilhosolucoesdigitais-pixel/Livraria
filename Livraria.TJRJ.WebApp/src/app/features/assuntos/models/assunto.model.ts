export interface IAssunto {
  codAs: number;
  descricao: string;
}

export interface IAssuntoCreateDto {
  descricao: string;
}

export interface IAssuntoUpdateDto {
  codAs: number;
  descricao: string;
}
