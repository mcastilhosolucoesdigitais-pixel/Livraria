import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AssuntoService } from '../services';
import { IAssunto } from '../models';
import { NotificationService } from '../../../core/services';

@Component({
  selector: 'app-assuntos-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './assuntos-list.page.html',
  styleUrls: ['./assuntos-list.page.scss']
})
export class AssuntosListPage implements OnInit {
  assuntos: IAssunto[] = [];
  loading = false;

  constructor(
    private assuntoService: AssuntoService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.loadAssuntos();
  }

  loadAssuntos(): void {
    this.loading = true;
    this.assuntoService.getAssuntos().subscribe({
      next: (data) => {
        this.assuntos = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  deleteAssunto(id: number, descricao: string): void {
    if (confirm(`Tem certeza que deseja excluir o assunto "${descricao}"?`)) {
      this.assuntoService.deleteAssunto(id).subscribe({
        next: () => {
          this.notificationService.showSuccess('Assunto excluído com sucesso!');
          this.loadAssuntos();
        }
      });
    }
  }
}
