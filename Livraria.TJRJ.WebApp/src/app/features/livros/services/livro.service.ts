import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILivro, ILivroCreateDto, ILivroUpdateDto } from '../models';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class LivroService {
  private apiUrl = `${environment.apiUrl}/livros`;

  constructor(private http: HttpClient) { }

  getLivros(): Observable<ILivro[]> {
    return this.http.get<ILivro[]>(this.apiUrl);
  }

  getLivroById(id: number): Observable<ILivro> {
    return this.http.get<ILivro>(`${this.apiUrl}/${id}`);
  }

  createLivro(livro: ILivroCreateDto): Observable<ILivro> {
    return this.http.post<ILivro>(this.apiUrl, livro);
  }

  updateLivro(id: number, livro: ILivroUpdateDto): Observable<ILivro> {
    return this.http.put<ILivro>(`${this.apiUrl}/${id}`, livro);
  }

  deleteLivro(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
