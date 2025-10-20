import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAssunto, IAssuntoCreateDto, IAssuntoUpdateDto } from '../models';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService {
  private apiUrl = `${environment.apiUrl}/assuntos`;

  constructor(private http: HttpClient) { }

  getAssuntos(): Observable<IAssunto[]> {
    return this.http.get<IAssunto[]>(this.apiUrl);
  }

  getAssuntoById(id: number): Observable<IAssunto> {
    return this.http.get<IAssunto>(`${this.apiUrl}/${id}`);
  }

  createAssunto(assunto: IAssuntoCreateDto): Observable<IAssunto> {
    return this.http.post<IAssunto>(this.apiUrl, assunto);
  }

  updateAssunto(id: number, assunto: IAssuntoUpdateDto): Observable<IAssunto> {
    return this.http.put<IAssunto>(`${this.apiUrl}/${id}`, assunto);
  }

  deleteAssunto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
