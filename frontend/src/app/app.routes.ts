import { Routes } from '@angular/router';
import { RegisterComponent } from './register/register';
import { LoginComponent } from './login/login';
import { DashboardComponent } from './dashboard/dashboard';

export const routes: Routes = [
  { path: '', redirectTo: '/register', pathMatch: 'full' },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent }
];
