import { IAutor } from '../../autores/models/autor.model';

export interface ILivroAutor {
  livro_Codl: number;
  autor_CodAu: number;
  autor?: IAutor;
}
