import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../services/auth/auth';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})

export class RegisterComponent {
  registerForm : FormGroup;
  loading = signal(false);
  errorMessage = signal<string>('');
  isRegistering = signal(false);

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required]],
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    })  
  }

  onSubmit() {
    this.isRegistering.set(true);
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe({
        next: () => {
          this.router.navigate(['/login'])
          this.isRegistering.set(false);
        },
        error: (err) => {
          this.errorMessage.set('User registration failed');
          // console.log('register error:', err);
        }
      })
    }
  }

  
}
