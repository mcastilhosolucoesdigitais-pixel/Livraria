import { Routes } from '@angular/router';
import { LivrosListPage } from './pages/livros-list.page';
import { LivroFormPage } from './pages/livro-form.page';

export const livrosRoutes: Routes = [
  {
    path: '',
    component: LivrosListPage
  },
  {
    path: 'novo',
    component: LivroFormPage
  },
  {
    path: 'editar/:id',
    component: LivroFormPage
  }
];
