import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AutorService } from '../services';
import { NotificationService } from '../../../core/services';
import { IAutorCreateDto, IAutorUpdateDto } from '../models';

@Component({
  selector: 'app-autor-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './autor-form.page.html',
  styleUrls: ['./autor-form.page.scss']
})
export class AutorFormPage implements OnInit {
  autorForm!: FormGroup;
  isEditMode = false;
  autorId?: number;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private autorService: AutorService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.initForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.autorId = Number(id);
      this.loadAutor(this.autorId);
    }
  }

  initForm(): void {
    this.autorForm = this.fb.group({
      nome: ['', [Validators.required, Validators.maxLength(40)]]
    });
  }

  loadAutor(id: number): void {
    this.loading = true;
    this.autorService.getAutorById(id).subscribe({
      next: (autor) => {
        this.autorForm.patchValue({
          nome: autor.nome
        });
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.autorForm.invalid) {
      this.markFormGroupTouched(this.autorForm);
      this.notificationService.showError('Por favor, preencha todos os campos obrigatórios.');
      return;
    }

    const formValue = this.autorForm.value;

    if (this.isEditMode && this.autorId) {
      const updateDto: IAutorUpdateDto = {
        codAu: this.autorId,
        ...formValue
      };

      this.autorService.updateAutor(this.autorId, updateDto).subscribe({
        next: () => {
          this.notificationService.showSuccess('Autor atualizado com sucesso!');
          this.router.navigate(['/autores']);
        }
      });
    } else {
      const createDto: IAutorCreateDto = formValue;

      this.autorService.createAutor(createDto).subscribe({
        next: () => {
          this.notificationService.showSuccess('Autor criado com sucesso!');
          this.router.navigate(['/autores']);
        }
      });
    }
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.autorForm.get(fieldName);
    return !!(field && field.invalid && field.touched);
  }

  getFieldError(fieldName: string): string {
    const field = this.autorForm.get(fieldName);
    if (field?.errors) {
      if (field.errors['required']) return 'Campo obrigatório';
      if (field.errors['maxlength']) return `Máximo de ${field.errors['maxlength'].requiredLength} caracteres`;
    }
    return '';
  }
}
