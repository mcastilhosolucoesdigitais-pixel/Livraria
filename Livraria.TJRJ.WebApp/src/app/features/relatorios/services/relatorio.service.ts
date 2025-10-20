import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IRelatorioLivrosPorAutor } from '../models';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class RelatorioService {
  private apiUrl = `${environment.apiUrl}/relatorios`;

  constructor(private http: HttpClient) { }

  getRelatorioLivrosPorAutor(): Observable<IRelatorioLivrosPorAutor[]> {
    return this.http.get<IRelatorioLivrosPorAutor[]>(`${this.apiUrl}/livros-por-autor`);
  }
}
