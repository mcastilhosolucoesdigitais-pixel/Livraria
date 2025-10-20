import { FormaDeCompra } from '../../../shared/models/forma-de-compra.enum';

export interface ILivroPreco {
  id: number;
  formaDeCompra: FormaDeCompra;
  preco: number;
}
