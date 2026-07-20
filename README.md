# TaskTracker 📝

A sleek, full-stack task management application built with an **Angular** frontend and a **.NET Web API** backend following Clean Architecture principles.

---

## 🛠️ Tech Stack

### **Frontend**
* **Framework:** Angular (Standalone Components, Signals)
* **Styling:** Tailwind CSS
* **HTTP Client:** RxJS & HttpClient

### **Backend**
* **Framework:** .NET 8 / 9 Web API
* **Architecture:** Clean Architecture (Domain, Application, Infrastructure, API)
* **ORM:** Entity Framework Core
* **Database:** PostgreSQL / SQL Server

---

## 🚀 Features

* **Task Dashboard:** View tasks sorted by status, priority, and due dates.
* **CRUD Operations:** Create, read, update, and delete tasks dynamically.
* **Reactive State:** Angular Signals for state management and UI updates.
* **Modal Forms:** Context-aware modals for adding and updating tasks.
* **RESTful API:** Robust backend handling domain rules and user isolation.

---

## 📁 Project Structure

```text
taskTracker/
├── backend/
│   ├── taskTracker.api/            # API Controllers & Middlewares
│   ├── taskTracker.Application/    # Use cases, Commands, Queries & DTOs
│   ├── taskTracker.Domain/         # Domain Entities & Enums
│   └── taskTracker.Infrastructure/ # EF Core DbContext & Repositories
└── frontend/
    └── src/
        ├── app/
        │   ├── components/         # Dashboard & Modal components
        │   └── services/           # Task API Service
        └── styles.css              # Global styles & Tailwind CSS
