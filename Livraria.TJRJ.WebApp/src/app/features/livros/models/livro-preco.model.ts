import { FormaDeCompra } from '../../../shared/models/forma-de-compra.enum';

export interface ILivroPreco {
  codl: number;
  formaDeCompra: FormaDeCompra;
  preco: number;
}
