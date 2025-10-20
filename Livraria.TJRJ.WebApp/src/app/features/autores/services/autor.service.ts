import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAutor, IAutorCreateDto, IAutorUpdateDto } from '../models';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AutorService {
  private apiUrl = `${environment.apiUrl}/autores`;

  constructor(private http: HttpClient) { }

  getAutores(): Observable<IAutor[]> {
    return this.http.get<IAutor[]>(this.apiUrl);
  }

  getAutorById(id: number): Observable<IAutor> {
    return this.http.get<IAutor>(`${this.apiUrl}/${id}`);
  }

  createAutor(autor: IAutorCreateDto): Observable<IAutor> {
    return this.http.post<IAutor>(this.apiUrl, autor);
  }

  updateAutor(id: number, autor: IAutorUpdateDto): Observable<IAutor> {
    return this.http.put<IAutor>(`${this.apiUrl}/${id}`, autor);
  }

  deleteAutor(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
