import { Component, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../auth';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})

export class LoginComponent {
  loginForm: FormGroup;
  errorMessage = signal<string>('');
  isLogging = signal(false)

  constructor(
    private fb: FormBuilder,
    private authService : AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    })
  }

  onSubmit() {
    this.isLogging.set(true);
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: () => {
          this.router.navigate(['/dashboard'])
          this.errorMessage.set('');
          this.isLogging.set(false);
        },
        error: (err) => {
          // console.log('login failed', err);
          this.errorMessage.set('Invalid Credentials!');
        }
      })
    }
  }
}
