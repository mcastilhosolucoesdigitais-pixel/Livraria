export enum FormaDeCompra {
  Balcao = 1,
  SelfService = 2,
  Internet = 3,
  Evento = 4
}

export const FormaDeCompraLabels: { [key in FormaDeCompra]: string } = {
  [FormaDeCompra.Balcao]: 'Balcão',
  [FormaDeCompra.SelfService]: 'Self-Service',
  [FormaDeCompra.Internet]: 'Internet',
  [FormaDeCompra.Evento]: 'Evento'
};
