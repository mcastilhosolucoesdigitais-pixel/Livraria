import { Routes } from '@angular/router';
import { AssuntosListPage } from './pages/assuntos-list.page';
import { AssuntoFormPage } from './pages/assunto-form.page';

export const assuntosRoutes: Routes = [
  {
    path: '',
    component: AssuntosListPage
  },
  {
    path: 'novo',
    component: AssuntoFormPage
  },
  {
    path: 'editar/:id',
    component: AssuntoFormPage
  }
];
