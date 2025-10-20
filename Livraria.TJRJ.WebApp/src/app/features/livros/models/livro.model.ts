import { IAssunto } from '../../assuntos/models/assunto.model';
import { ILivroAutor } from './livro-autor.model';
import { ILivroPreco } from './livro-preco.model';
import { FormaDeCompra } from '../../../shared/models/forma-de-compra.enum';

export interface ILivro {
  id: number;
  titulo: string;
  editora: string;
  edicao: number;
  anoPublicacao: string;
  assuntos: IAssunto[];
  autores: ILivroAutor[];
  precos: ILivroPreco[];
}

export interface IPrecoInput {
  valor: number;
  formaDeCompra: string;
}

export interface ILivroCreateDto {
  titulo: string;
  editora: string;
  edicao: number;
  anoPublicacao: string;
  assuntos: number[];
  autores: number[];
  precos: IPrecoInput[];
}

export interface ILivroUpdateDto {
  id: number;
  titulo: string;
  editora: string;
  edicao: number;
  anoPublicacao: string;
  assuntos: number[];
  autores: number[];
  precos: IPrecoInput[];
}
