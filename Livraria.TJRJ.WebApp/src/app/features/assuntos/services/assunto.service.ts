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

  getAssuntoById(codAs: number): Observable<IAssunto> {
    return this.http.get<IAssunto>(`${this.apiUrl}/${codAs}`);
  }

  createAssunto(assunto: IAssuntoCreateDto): Observable<IAssunto> {
    return this.http.post<IAssunto>(this.apiUrl, assunto);
  }

  updateAssunto(codAs: number, assunto: IAssuntoUpdateDto): Observable<IAssunto> {
    return this.http.put<IAssunto>(`${this.apiUrl}/${codAs}`, assunto);
  }

  deleteAssunto(codAs: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${codAs}`);
  }
}
