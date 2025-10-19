import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RelatorioService } from '../services';
import { IRelatorioLivrosPorAutor } from '../models';

@Component({
  selector: 'app-relatorio-livros-por-autor',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './relatorio-livros-por-autor.page.html',
  styleUrls: ['./relatorio-livros-por-autor.page.scss']
})
export class RelatorioLivrosPorAutorPage implements OnInit {
  relatorio: IRelatorioLivrosPorAutor[] = [];
  loading = false;

  constructor(private relatorioService: RelatorioService) {}

  ngOnInit(): void {
    this.loadRelatorio();
  }

  loadRelatorio(): void {
    this.loading = true;
    this.relatorioService.getRelatorioLivrosPorAutor().subscribe({
      next: (data) => {
        this.relatorio = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  getTotalLivrosPorAutor(autor: IRelatorioLivrosPorAutor): number {
    return autor.livros.length;
  }

  print(): void {
    window.print();
  }
}
