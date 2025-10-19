export enum FormaDeCompra {
  Balcao = 0,
  SelfService = 1,
  Internet = 2,
  Evento = 3
}

export const FormaDeCompraLabels: { [key in FormaDeCompra]: string } = {
  [FormaDeCompra.Balcao]: 'Balcão',
  [FormaDeCompra.SelfService]: 'Self-Service',
  [FormaDeCompra.Internet]: 'Internet',
  [FormaDeCompra.Evento]: 'Evento'
};
