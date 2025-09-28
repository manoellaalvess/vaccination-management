# Vaccination Management

Vaccination card management system developed in C# .NET, with SQLite as the database.
This project was built following best practices in layered architecture (Domain, Application, Infrastructure, CrossCutting and API) and using MediatR to implement the CQRS pattern.

---

## Features

- **Person**
  - Creates a person
  - Get all people
  - Get a person by CPF (with the vaccination history)
  - Deletes a person (also deleting their vaccination history)

- **Vaccine**
  - Adds a vaccine to the database
  - Get all vaccines

- **Vaccination**
  - Adds a vaccination record to a person
  - Consultar cartão de vacinação de uma pessoa
  - Excluir registro de vacinação

---

## 🛠️ Tecnologias Utilizadas

- **.NET 9 (C#)**  
- **ASP.NET Core Web API**  
- **Entity Framework Core + SQLite**  
- **MediatR** (CQRS + Mediator Pattern)  
- **Swagger** for API documentation  

---

## ⚙️ How to build the project for the first time:

### 1. Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- VS Code ou Visual Studio  
- Postman/Insomnia (opcional, for API tests)

### 2. Clone the repository
```bash
git clone https://github.com/seu-usuario/vaccination-management.git
cd vaccination-management

### 3. Restore the packages
dotnet restore

### 4. Create the database
dotnet ef migrations add InitialCreate -p src/VaccinationManagement.Infrastructure -s src/VaccinationManagement.Api
dotnet ef database update -p src/VaccinationManagement.Infrastructure -s src/VaccinationManagement.Api

### 5. Build the application
dotnet run --project src/VaccinationManagement.Api

Access the documentation by http://localhost:5218/swagger/index.html