import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { LivroService } from '../services';
import { AutorService } from '../../autores/services';
import { AssuntoService } from '../../assuntos/services';
import { NotificationService } from '../../../core/services';
import { IAutor } from '../../autores/models';
import { IAssunto } from '../../assuntos/models';
import { FormaDeCompra, FormaDeCompraLabels } from '../../../shared/models';
import { ILivroCreateDto, ILivroUpdateDto } from '../models';

@Component({
  selector: 'app-livro-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './livro-form.page.html',
  styleUrls: ['./livro-form.page.scss']
})
export class LivroFormPage implements OnInit {
  livroForm!: FormGroup;
  isEditMode = false;
  livroId?: number;
  loading = false;

  autores: IAutor[] = [];
  assuntos: IAssunto[] = [];
  formasDeCompra = Object.keys(FormaDeCompra)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: FormaDeCompraLabels[Number(key) as FormaDeCompra]
    }));

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private livroService: LivroService,
    private autorService: AutorService,
    private assuntoService: AssuntoService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadAutores();
    this.loadAssuntos();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.livroId = Number(id);
      this.loadLivro(this.livroId);
    }
  }

  initForm(): void {
    this.livroForm = this.fb.group({
      titulo: ['', [Validators.required, Validators.maxLength(40)]],
      editora: ['', [Validators.required, Validators.maxLength(40)]],
      edicao: ['', [Validators.required, Validators.min(1)]],
      anoPublicacao: ['', [Validators.required, Validators.pattern(/^\d{4}$/)]],
      autores: [[], [Validators.required]],
      assuntos: [[], [Validators.required]],
      valor: ['', [Validators.required, Validators.min(0.01)]],
      formaDeCompra: [FormaDeCompra.Balcao, [Validators.required]]
    });
  }

  loadAutores(): void {
    this.autorService.getAutores().subscribe({
      next: (data) => {
        this.autores = data;
      }
    });
  }

  loadAssuntos(): void {
    this.assuntoService.getAssuntos().subscribe({
      next: (data) => {
        this.assuntos = data;
      }
    });
  }

  loadLivro(id: number): void {
    this.loading = true;
    this.livroService.getLivroById(id).subscribe({
      next: (livro) => {
        const preco = livro.precos && livro.precos.length > 0 ? livro.precos[0] : null;

        this.livroForm.patchValue({
          titulo: livro.titulo,
          editora: livro.editora,
          edicao: livro.edicao,
          anoPublicacao: livro.anoPublicacao,
          autores: livro.autores.map(la => la.autor_CodAu),
          assuntos: livro.assuntos.map(a => a.codAs),
          valor: preco ? preco.preco : 0,
          formaDeCompra: preco ? preco.formaDeCompra : FormaDeCompra.Balcao
        });
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  onAutorChange(event: Event, codAu: number): void {
    const checkbox = event.target as HTMLInputElement;
    const autoresSelecionados = this.livroForm.get('autores')?.value || [];

    if (checkbox.checked) {
      autoresSelecionados.push(codAu);
    } else {
      const index = autoresSelecionados.indexOf(codAu);
      if (index > -1) {
        autoresSelecionados.splice(index, 1);
      }
    }

    this.livroForm.patchValue({ autores: autoresSelecionados });
  }

  onAssuntoChange(event: Event, codAs: number): void {
    const checkbox = event.target as HTMLInputElement;
    const assuntosSelecionados = this.livroForm.get('assuntos')?.value || [];

    if (checkbox.checked) {
      assuntosSelecionados.push(codAs);
    } else {
      const index = assuntosSelecionados.indexOf(codAs);
      if (index > -1) {
        assuntosSelecionados.splice(index, 1);
      }
    }

    this.livroForm.patchValue({ assuntos: assuntosSelecionados });
  }

  isAutorSelected(codAu: number): boolean {
    const autoresSelecionados = this.livroForm.get('autores')?.value || [];
    return autoresSelecionados.includes(codAu);
  }

  isAssuntoSelected(codAs: number): boolean {
    const assuntosSelecionados = this.livroForm.get('assuntos')?.value || [];
    return assuntosSelecionados.includes(codAs);
  }

  onSubmit(): void {
    if (this.livroForm.invalid) {
      this.markFormGroupTouched(this.livroForm);
      this.notificationService.showError('Por favor, preencha todos os campos obrigatórios.');
      return;
    }

    const formValue = this.livroForm.value;

    if (this.isEditMode && this.livroId) {
      const updateDto: ILivroUpdateDto = {
        codl: this.livroId,
        ...formValue
      };

      this.livroService.updateLivro(this.livroId, updateDto).subscribe({
        next: () => {
          this.notificationService.showSuccess('Livro atualizado com sucesso!');
          this.router.navigate(['/livros']);
        }
      });
    } else {
      const createDto: ILivroCreateDto = formValue;

      this.livroService.createLivro(createDto).subscribe({
        next: () => {
          this.notificationService.showSuccess('Livro criado com sucesso!');
          this.router.navigate(['/livros']);
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
    const field = this.livroForm.get(fieldName);
    return !!(field && field.invalid && field.touched);
  }

  getFieldError(fieldName: string): string {
    const field = this.livroForm.get(fieldName);
    if (field?.errors) {
      if (field.errors['required']) return 'Campo obrigatório';
      if (field.errors['maxlength']) return `Máximo de ${field.errors['maxlength'].requiredLength} caracteres`;
      if (field.errors['min']) return `Valor mínimo: ${field.errors['min'].min}`;
      if (field.errors['pattern']) return 'Formato inválido';
    }
    return '';
  }
}
