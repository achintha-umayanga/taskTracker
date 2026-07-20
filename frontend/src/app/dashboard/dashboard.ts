import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CreateTaskRequest, TaskPriority, TaskResponse, TaskService, TaskStatus } from '../services/task';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class DashboardComponent implements OnInit {
  private readonly taskService = inject(TaskService);

  TaskStatus = TaskStatus;
  TaskPriority = TaskPriority;

  tasks = signal<TaskResponse[]>([]);
  isLoading = signal(false);
  errorMessage = signal<string>('');

  isTaskCreateModalOpen = signal(false);
  isSubmitting = signal(false);
  editingTaskId = signal<string | null>(null);

  createName = signal<string>('');
  createStatus= signal<TaskStatus>(TaskStatus.ToDo);
  createDueDate = signal<string>('');
  createPriority = signal<TaskPriority>(TaskPriority.Low);

  modalTitle = computed(() => (
    this.editingTaskId() ? 'Edit Task' : 'Add Task'
  ));

  // initiate fetch tasks immediately
  ngOnInit(): void {
    this.fetchTasks();
  }

  // fetch tasks
  fetchTasks(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.taskService.getAllTasks().subscribe({
      next: (data) => {
        this.tasks.set(data ?? []);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.log('failed to fetch tasks:', err);
        this.errorMessage.set('Could not load tasks from the backend');
        this.isLoading.set(false);
      }
    })
  }

  // delete task
  deleteTask(id: string): void {
    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.fetchTasks();
        console.log('task deleted');
      },
      error: () => {
        console.log('task delete failed');
      }
    })
  }

  // create task
  createTaskModalOpen(): void {
    this.editingTaskId.set(null)
    this.createName.set('');
    this.createStatus.set(TaskStatus.ToDo);
    this.createDueDate.set(new Date().toISOString().slice(0, 10));
    this.createPriority.set(TaskPriority.Low);
    this.isTaskCreateModalOpen.set(true);
  }

  updateTaskModalOpen(task: TaskResponse): void {
    this.editingTaskId.set(task.id)
    this.createName.set(task.name);
    this.createStatus.set(task.status);
    this.createDueDate.set(task.dueDate);
    this.createPriority.set(task.priority);
    this.isTaskCreateModalOpen.set(true);
  }

  createTaskModalClose(): void {
    this.isTaskCreateModalOpen.set(false);
    this.editingTaskId.set(null);
  }

  createAndUpdateTask(): void {
    this.isSubmitting.set(true)

    const payload: CreateTaskRequest = {
      name: this.createName().trim(),
      status: Number(this.createStatus()) as TaskStatus,
      dueDate: this.createDueDate(),
      priority: Number(this.createPriority()) as TaskPriority
    }

    const taskId = this.editingTaskId();

    if (taskId) {
      this.taskService.updateTask(taskId, payload).subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.createTaskModalClose();
          this.fetchTasks()
        },
        error: (err) => {
          console.log('failed to update task:', err)
          this.isSubmitting.set(false);
        }
      })
    } else {
      this.taskService.createTask(payload).subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.createTaskModalClose();
          this.fetchTasks()
        },
        error: (err) => {
          console.log('failed to create task:', err)
          this.isSubmitting.set(false);
        }
      })
    }
  }
}
