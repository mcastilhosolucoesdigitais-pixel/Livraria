export interface IAssunto {
  id: number;
  descricao: string;
}

export interface IAssuntoCreateDto {
  descricao: string;
}

export interface IAssuntoUpdateDto {
  id: number;
  descricao: string;
}
