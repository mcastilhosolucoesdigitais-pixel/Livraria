import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/livros',
    pathMatch: 'full'
  },
  {
    path: 'livros',
    loadChildren: () => import('./features/livros/livros.routes').then(m => m.livrosRoutes)
  },
  {
    path: 'autores',
    loadChildren: () => import('./features/autores/autores.routes').then(m => m.autoresRoutes)
  },
  {
    path: 'assuntos',
    loadChildren: () => import('./features/assuntos/assuntos.routes').then(m => m.assuntosRoutes)
  },
  {
    path: 'relatorios',
    loadChildren: () => import('./features/relatorios/relatorios.routes').then(m => m.relatoriosRoutes)
  }
];
