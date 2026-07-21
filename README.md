# 🏥 Hospital Management System

A comprehensive hospital management system built with **.NET 10** and **C#**, designed for Kenyan healthcare facilities to manage patient records, doctor profiles, appointments, medical records, and prescriptions—fully compliant with Kenya's **2026 Digital Health Act** and data protection regulations.

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)
- [Kenya Compliance](#kenya-compliance)
- [Contributing](#contributing)
- [License](#license)

## ✨ Features

- **Patient Management**
  - Register and manage patient profiles
  - National ID validation (Kenya IPRS compliance)
  - SHA (formerly NHIF) insurance tracking
  - Data processing consent management
  - Medical records and history

- **Doctor Management**
  - Doctor profiles with KMPDC license numbers
  - Specialization and qualification tracking
  - Availability status and consultation fees
  - Schedule management

- **Appointment System**
  - Book and manage appointments
  - Appointment status tracking (CheckedIn, CheckedOut, Cancelled, etc.)
  - Doctor and patient associations
  - Appointment codes for quick lookup

- **Medical Records & Prescriptions**
  - Maintain detailed medical records
  - Prescription management with drug tracking
  - Audit trail for compliance

- **API Documentation**
  - Swagger/OpenAPI integration
  - Interactive API testing

## 🛠 Tech Stack

| Layer | Technology |
|-------|------------|
| **Framework** | ASP.NET Core 10.0 |
| **Language** | C# 12 |
| **Database** | SQL Server with Entity Framework Core 10.0 |
| **ORM** | Entity Framework Core |
| **Validation** | FluentValidation 11.11.0 |
| **Mapping** | AutoMapper 13.0.1 |
| **CQRS Pattern** | MediatR 12.2.0 |
| **Security** | BCrypt.Net 4.0.3, JWT Bearer |
| **API Documentation** | Swashbuckle 10.1.0 |

## 🏗 Architecture

This project follows **Clean Architecture** with **CQRS (Command Query Responsibility Segregation)** pattern:

```
HospitalSystem.API (Presentation Layer)
    ↓
HospitalSystem.Application (Business Logic - CQRS)
    ↓
HospitalSystem.Domain (Core Domain & Entities)
    ↓
HospitalSystem.Infrastructure (Data Access & External Services)
    ↓
SQL Server Database
```

### Layer Responsibilities

- **Domain**: Core business entities, domain logic, and rules (Patient, Doctor, Appointment, etc.)
- **Application**: Use cases, command handlers, query handlers, and DTOs
- **Infrastructure**: Database context, repositories, and external service integrations
- **API**: Controllers, request/response handling, and HTTP routing

## 🚀 Getting Started

### Prerequisites

- **.NET 10 SDK** or later ([Download](https://dotnet.microsoft.com/download))
- **SQL Server** 2019 or later (or SQL Server Express)
- **Visual Studio 2022** or **VS Code** with C# extension (optional)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/GHANNAWAY-TECH/HospitalManagementSystem.git
   cd HospitalManagementSystem
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

### Database Setup

1. **Ensure SQL Server is running** on your local machine or update the connection string in `appsettings.json`

2. **Configure the connection string** (default uses local SQL Server with Windows authentication):
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=HospitalSystem;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

3. **Create and seed the database:**
   ```bash
   # Create migrations (if not already done)
   dotnet ef migrations add InitialCreate --project HospitalSystem.Infrastructure

   # Apply migrations to database
   dotnet ef database update --project HospitalSystem.Infrastructure
   ```

4. **Run the application:**
   ```bash
   dotnet run --project HospitalSystem.API
   ```

5. **Access the API:**
   - **Swagger UI**: http://localhost:5000/swagger
   - **API Base URL**: http://localhost:5000/api

## 📁 Project Structure

```
HospitalManagementSystem/
├── HospitalSystem.API/                 # ASP.NET Core Web API
│   ├── Controllers/                    # HTTP endpoints
│   ├── Program.cs                      # Service registration & middleware
│   ├── appsettings.json               # Configuration
│   └── Properties/
│
├── HospitalSystem.Domain/              # Core business domain
│   ├── Entities/                       # Patient, Doctor, Appointment, etc.
│   │   ├── Patient.cs
│   │   ├── Doctor.cs
│   │   ├── Appointment.cs
│   │   ├── MedicalRecord.cs
│   │   ├── Prescription.cs
│   │   └── Drug.cs
│   ├── Common/                         # Shared base classes
│   │   └── BaseEntity.cs
│   └── Enums/                          # Appointment status, etc.
│
├── HospitalSystem.Application/         # Business logic & CQRS
│   ├── Patients/                       # Patient commands and queries
│   │   ├── Commands/                   # Create, Update, Delete commands
│   │   └── DTOs/                       # Data transfer objects
│   ├── Doctors/                        # Doctor operations
│   ├── Appointments/                   # Appointment scheduling
│   ├── Common/                         # Interfaces and shared logic
│   ├── Mappings/                       # AutoMapper profiles
│   └── HospitalSystem.Application.csproj
│
├── HospitalSystem.Infrastructure/      # Data access & services
│   ├── Data/
│   │   └── ApplicationDbContext.cs    # EF Core DbContext
│   ├── DependencyInjection.cs         # Service registration
│   └── HospitalSystem.Infrastructure.csproj
│
├── HospitalSystem.Shared/              # Shared utilities
│   └── Constants, Helpers, Extensions
│
└── HospitalSystem.slnx                # Solution file
```

## 🔌 API Endpoints

### Patients
- `GET /api/patients` - List all patients
- `GET /api/patients/{id}` - Get patient details
- `POST /api/patients` - Create new patient
- `PUT /api/patients/{id}` - Update patient
- `DELETE /api/patients/{id}` - Delete patient

### Doctors
- `GET /api/doctors` - List all doctors
- `GET /api/doctors/{id}` - Get doctor details
- `POST /api/doctors` - Create new doctor
- `PUT /api/doctors/{id}` - Update doctor

### Appointments
- `GET /api/appointments` - List appointments
- `POST /api/appointments` - Book appointment
- `PATCH /api/appointments/{id}/status` - Update appointment status

View the complete API documentation at `/swagger` after running the application.

## ⚙️ Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=HospitalSystem;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### appsettings.Development.json

Used for development environment overrides.

## 🇰🇪 Kenya Compliance

This system is built with Kenya's healthcare regulations in mind:

### Kenya Digital Health Act 2026
- **National ID Tracking**: Unique NationalId index prevents duplicate patient registrations
- **IPRS Verification**: Mandatory national ID field for patient identification
- **SHA Insurance**: Support for SHA (formerly NHIF) insurance number tracking
- **Data Processing Consent**: Mandatory consent field for data digitization compliance with Kenya Data Protection Act

### Medical Practice Standards
- **KMPDC License Numbers**: All doctors must have valid Kenya Medical Practitioners and Dentists Council (KMPDC) license numbers
- **Audit Trail**: All patient data changes are auditable and tracked
- **Medical Records**: Comprehensive patient medical history with restricted deletion policies

## 🔐 Security Features

- Password hashing with BCrypt.Net
- JWT Bearer authentication ready
- CORS policy configuration
- Data processing consent tracking
- Role-based access control ready

## 📝 Notes

- Sample patient and doctor records are auto-seeded on first application run
- Patient codes are auto-generated in the format `HOSP-000001`
- Doctor codes are auto-generated in the format `DOC-001`
- Appointment status is stored as string enums in the database for better auditability

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is open source and available under the MIT License.

---

**Built with ❤️ for Kenyan healthcare facilities**

For issues, questions, or suggestions, please open an issue on GitHub.
