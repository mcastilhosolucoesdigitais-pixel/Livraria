import { Routes } from '@angular/router';
import { AutoresListPage } from './pages/autores-list.page';
import { AutorFormPage } from './pages/autor-form.page';

export const autoresRoutes: Routes = [
  {
    path: '',
    component: AutoresListPage
  },
  {
    path: 'novo',
    component: AutorFormPage
  },
  {
    path: 'editar/:id',
    component: AutorFormPage
  }
];
