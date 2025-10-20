import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { NotificationService } from '../services/notification.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const notificationService = inject(NotificationService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'Ocorreu um erro desconhecido.';

      if (error.error instanceof ErrorEvent) {
        // Erro do lado do cliente ou de rede
        errorMessage = `Erro: ${error.error.message}`;
      } else {
        // Erro do lado do servidor
        if (error.status === 404) {
          errorMessage = `Recurso não encontrado: ${error.url}`;
        } else if (error.error && error.error.detail) {
          errorMessage = error.error.detail; // Detalhe do ProblemDetails da API
        }
         else if (error.status === 400 && error.error?.errors) {
          // Erros de validação da API
          const validationErrors = Object.values(error.error.errors)
            .flat()
            .join(' ');
          errorMessage = validationErrors;
        } 
        else if (error.error && typeof error.error === 'string') {
          errorMessage = error.error;
        } else if (error.status === 0) {
          errorMessage = 'Não foi possível conectar ao servidor. Verifique sua conexão.';
        } else {
          errorMessage = `Erro ${error.status}: ${error.message}`;
        }
      }

      notificationService.showError(errorMessage);
      return throwError(() => new Error(errorMessage));
    })
  );
};
