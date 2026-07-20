import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface CreateTaskRequest {
  name: string;
  status: TaskStatus;
  dueDate: string;
  priority: TaskPriority;
}

export interface UpdateTaskRequest {
  name: string;
  status: TaskStatus;
  dueDate: string;
  priority: TaskPriority;
}

export interface TaskResponse {
  id: string;
  key: string;
  name: string;
  status: TaskStatus;
  dueDate: string;
  priority: TaskPriority;
  userId: string;
}

export enum TaskStatus {
  ToDo = 1,
  InProgress = 2,
  BackLog = 3,
  Done = 4
}

export enum TaskPriority {
  Normal = 1,
  Urgent = 2,
  Low = 3
}

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private readonly apiUrl = 'http://localhost:5066/api/task';

  constructor(private readonly http: HttpClient) {}

  createTask(payload: CreateTaskRequest): Observable<TaskResponse> {
    return this.http.post<TaskResponse>(`${this.apiUrl}/createTask`, payload);
  }

  updateTask(id: string, payload: UpdateTaskRequest): Observable<TaskResponse> {
    return this.http.put<TaskResponse>(`${this.apiUrl}/updateTask/${id}`, payload);
  }

  deleteTask(id: string): Observable< {message: string} > {
    return this.http.delete<{ message: string }>(`${this.apiUrl}/deleteTask/${id}`);
  }

  getAllTasks(): Observable<TaskResponse[]> {
    return this.http.get<TaskResponse[]>(`${this.apiUrl}/getAllTasks`);
  }
}
