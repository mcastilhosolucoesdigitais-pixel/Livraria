import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LivroService } from '../services';
import { ILivro } from '../models';
import { NotificationService } from '../../../core/services';

@Component({
  selector: 'app-livros-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './livros-list.page.html',
  styleUrls: ['./livros-list.page.scss']
})
export class LivrosListPage implements OnInit {
  livros: ILivro[] = [];
  loading = false;

  constructor(
    private livroService: LivroService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.loadLivros();
  }

  loadLivros(): void {
    this.loading = true;
    this.livroService.getLivros().subscribe({
      next: (data) => {
        this.livros = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  deleteLivro(id: number, titulo: string): void {
    if (confirm(`Tem certeza que deseja excluir o livro "${titulo}"?`)) {
      this.livroService.deleteLivro(id).subscribe({
        next: () => {
          this.notificationService.showSuccess('Livro excluído com sucesso!');
          this.loadLivros();
        }
      });
    }
  }

  getAutoresNomes(livro: ILivro): string {
    return livro.autores
      .map(la => la.nome || '')
      .filter(nome => nome)
      .join(', ');
  }

  getAssuntosDescricoes(livro: ILivro): string {
    return livro.assuntos
      .map(a => a.descricao)
      .join(', ');
  }
}
