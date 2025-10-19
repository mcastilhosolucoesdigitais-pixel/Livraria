import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AssuntoService } from '../services';
import { NotificationService } from '../../../core/services';
import { IAssuntoCreateDto, IAssuntoUpdateDto } from '../models';

@Component({
  selector: 'app-assunto-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './assunto-form.page.html',
  styleUrls: ['./assunto-form.page.scss']
})
export class AssuntoFormPage implements OnInit {
  assuntoForm!: FormGroup;
  isEditMode = false;
  assuntoId?: number;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private assuntoService: AssuntoService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.initForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.assuntoId = Number(id);
      this.loadAssunto(this.assuntoId);
    }
  }

  initForm(): void {
    this.assuntoForm = this.fb.group({
      descricao: ['', [Validators.required, Validators.maxLength(20)]]
    });
  }

  loadAssunto(id: number): void {
    this.loading = true;
    this.assuntoService.getAssuntoById(id).subscribe({
      next: (assunto) => {
        this.assuntoForm.patchValue({
          descricao: assunto.descricao
        });
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.assuntoForm.invalid) {
      this.markFormGroupTouched(this.assuntoForm);
      this.notificationService.showError('Por favor, preencha todos os campos obrigatórios.');
      return;
    }

    const formValue = this.assuntoForm.value;

    if (this.isEditMode && this.assuntoId) {
      const updateDto: IAssuntoUpdateDto = {
        codAs: this.assuntoId,
        ...formValue
      };

      this.assuntoService.updateAssunto(this.assuntoId, updateDto).subscribe({
        next: () => {
          this.notificationService.showSuccess('Assunto atualizado com sucesso!');
          this.router.navigate(['/assuntos']);
        }
      });
    } else {
      const createDto: IAssuntoCreateDto = formValue;

      this.assuntoService.createAssunto(createDto).subscribe({
        next: () => {
          this.notificationService.showSuccess('Assunto criado com sucesso!');
          this.router.navigate(['/assuntos']);
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
    const field = this.assuntoForm.get(fieldName);
    return !!(field && field.invalid && field.touched);
  }

  getFieldError(fieldName: string): string {
    const field = this.assuntoForm.get(fieldName);
    if (field?.errors) {
      if (field.errors['required']) return 'Campo obrigatório';
      if (field.errors['maxlength']) return `Máximo de ${field.errors['maxlength'].requiredLength} caracteres`;
    }
    return '';
  }
}
