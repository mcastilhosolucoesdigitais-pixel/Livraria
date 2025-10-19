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

  getLivroById(codl: number): Observable<ILivro> {
    return this.http.get<ILivro>(`${this.apiUrl}/${codl}`);
  }

  createLivro(livro: ILivroCreateDto): Observable<ILivro> {
    return this.http.post<ILivro>(this.apiUrl, livro);
  }

  updateLivro(codl: number, livro: ILivroUpdateDto): Observable<ILivro> {
    return this.http.put<ILivro>(`${this.apiUrl}/${codl}`, livro);
  }

  deleteLivro(codl: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${codl}`);
  }
}
