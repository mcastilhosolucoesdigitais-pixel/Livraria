import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AutorService } from '../services';
import { IAutor } from '../models';
import { NotificationService } from '../../../core/services';

@Component({
  selector: 'app-autores-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './autores-list.page.html',
  styleUrls: ['./autores-list.page.scss']
})
export class AutoresListPage implements OnInit {
  autores: IAutor[] = [];
  loading = false;

  constructor(
    private autorService: AutorService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.loadAutores();
  }

  loadAutores(): void {
    this.loading = true;
    this.autorService.getAutores().subscribe({
      next: (data) => {
        this.autores = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  deleteAutor(id: number, nome: string): void {
    if (confirm(`Tem certeza que deseja excluir o autor "${nome}"?`)) {
      this.autorService.deleteAutor(id).subscribe({
        next: () => {
          this.notificationService.showSuccess('Autor excluído com sucesso!');
          this.loadAutores();
        }
      });
    }
  }
}
