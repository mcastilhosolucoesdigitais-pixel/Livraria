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
  }
];
