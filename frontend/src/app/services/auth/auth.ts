import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

export interface RegisterRequest {
  email: string;
  password: string;
  username: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  id: string;
  userName: string;
  email: string;
  token: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = 'http://localhost:5066/api/auth';

  constructor(private readonly http: HttpClient) {}

  register(payload: RegisterRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, payload);
  }

  login(payload: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, payload).pipe(
      tap((response) => {
        localStorage.setItem('authToken', response.token);
        localStorage.setItem('User_Data', JSON.stringify({ userId: response.id, username: response.userName, email: response.email }));
      })
    )
  }
}
