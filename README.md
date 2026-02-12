# AYA University Information System (UIS)

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![React](https://img.shields.io/badge/React-19.2.1-61DAFB.svg)](https://reactjs.org/)
[![C#](https://img.shields.io/badge/C%23-12.0-purple.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019+-red.svg)](https://www.microsoft.com/en-us/sql-server)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

## 🎯 Overview

AYA-UIS is a comprehensive, modern university information system designed to streamline academic operations and administrative processes. The system provides a unified platform for managing departments, courses, academic schedules, fee structures, and user authentication with role-based access control.

## ✨ Key Features

### 🏛️ Academic Management
- **Department Management**: Complete CRUD operations for academic departments with codes and descriptions
- **Course Management**: Course creation, modification, and credit hour management
- **Academic Schedule Management**: Comprehensive scheduling system for academic activities
- **Course Upload System**: File upload and management capabilities for course materials

### 💰 Financial Management  
- **Fee Structure Management**: Dynamic fee type creation and amount management
- **Department-Specific Fees**: Customizable fee structures per academic department
- **Financial Reporting**: Track and manage university financial operations

### 👥 User Management & Security
- **Role-Based Access Control (RBAC)**: Hierarchical permission system
- **JWT Authentication**: Secure token-based authentication with RSA encryption
- **User Registration & Login**: Comprehensive authentication flow
- **Password Management**: Secure password reset and recovery system
- **Multi-Role Support**: Students, Faculty, Administrators, and custom roles

### 🔐 Security Features
- **RSA Key Encryption**: Public/private key authentication system
- **Rate Limiting**: API endpoint protection against abuse
- **Global Exception Handling**: Comprehensive error management
- **Data Validation**: Input sanitization and validation at all levels

## 🏗️ System Architecture

### High-Level Architecture
```
┌─────────────────────────────────────────────────────────────┐
│                    Frontend (React)                         │
│                Modern SPA Interface                         │
│            Component-Based Architecture                     │
└─────────────────────────────────────────────────────────────┘
                            │
                       HTTP/REST API
                            │
┌─────────────────────────────────────────────────────────────┐
│                    API Gateway                              │
│                  Authentication                             │
│               Rate Limiting & Security                      │
└─────────────────────────────────────────────────────────────┘
                            │
┌─────────────────────────────────────────────────────────────┐
│                 Backend (.NET 8 API)                       │
│                Clean Architecture                           │
│                   CQRS Pattern                             │
└─────────────────────────────────────────────────────────────┘
                            │
┌──────────────────────┬──────────────────────────────────────┐
│     SQL Server       │          Identity Store              │
│   Main Database      │       (User Management)             │
│                      │                                      │
└──────────────────────┴──────────────────────────────────────┘
```

### Backend Architecture (Clean Architecture + CQRS)
```
┌─────────────────────────────────────────────────────────────┐
│                        API Layer                             │
│                    (AYA_UIS.API)                            │
│         Controllers, Middleware, Authentication             │
└─────────────────────────────────────────────────────────────┘
                            │
┌─────────────────────────────────────────────────────────────┐
│                   Application Layer                          │
│                (AYA_UIS.Application)                        │
│        Commands, Queries, Handlers (CQRS)                   │
│              Business Logic & Use Cases                     │
└─────────────────────────────────────────────────────────────┘
                            │
┌─────────────────────────────────────────────────────────────┐
│                      Core Layer                             │
│                   (AYA_UIS.Core)                           │
│           Domain Entities, Services, Contracts              │
│                  Domain Business Rules                      │
└─────────────────────────────────────────────────────────────┘
                            │
┌─────────────────────────────────────────────────────────────┐
│                 Infrastructure Layer                        │
│                (AYA_UIS.Infrastructure)                     │
│        Data Access, External Services, Repositories         │
└─────────────────────────────────────────────────────────────┘
```

## 🚀 Technology Stack

### Backend Technologies
- **Framework**: ASP.NET Core 8.0
- **Language**: C# 12.0
- **Architecture**: Clean Architecture with CQRS
- **Database**: SQL Server 2019+
- **ORM**: Entity Framework Core
- **Authentication**: JWT with RSA encryption
- **Identity**: ASP.NET Core Identity
- **API Documentation**: Swagger/OpenAPI
- **Dependency Injection**: Built-in .NET DI Container

### Frontend Technologies
- **Framework**: React 19.2.1
- **Language**: JavaScript/TypeScript (planned)
- **Build Tool**: Create React App
- **Package Manager**: npm
- **Testing**: Jest, React Testing Library

### Development & DevOps
- **Version Control**: Git
- **Development Environment**: Visual Studio Code / Visual Studio
- **Database Management**: SQL Server Management Studio
- **API Testing**: HTTP files, Postman-compatible

## 📁 Project Structure

### Root Structure
```
AYA-UIS/
├── AYA_UIS_Frontend/          # React frontend application
├── AYA_UIS_Server/            # .NET backend solution
├── README.md                  # Main documentation
└── docs/                      # Additional documentation
```

### Backend Structure (`AYA_UIS_Server/`)
```
AYA_UIS_Server/
├── AYA_UIS.sln               # Solution file
├── AYA_UIS.API/              # Web API layer
│   ├── Controllers/          # API controllers
│   ├── MiddelWares/          # Custom middleware
│   ├── Factories/            # Response factories
│   └── Keys/                 # RSA encryption keys
├── AYA_UIS.Application/      # Application layer (CQRS)
│   ├── Commands/             # Command handlers
│   ├── Queries/              # Query handlers
│   ├── Handlers/             # Business logic handlers
│   └── Contracts/            # Service contracts
├── AYA_UIS.Core/             # Domain layer
│   ├── Domain/               # Domain entities
│   ├── Abstractions/         # Interfaces
│   └── Services/             # Domain services
├── AYA_UIS.Infrastructure/   # Infrastructure layer
│   ├── Persistence/          # Database context & repositories
│   └── Services/             # External service implementations
└── Shared/                   # Shared DTOs and models
    ├── Dtos/                 # Data transfer objects
    ├── Common/               # Common utilities
    └── Responses/            # API response models
```

### Frontend Structure (Professional - To Be Implemented)
```
AYA_UIS_Frontend/
├── public/                   # Static assets
├── src/
│   ├── components/           # Reusable UI components
│   │   ├── common/          # Generic components
│   │   ├── layout/          # Layout components
│   │   └── forms/           # Form components
│   ├── pages/               # Page components
│   │   ├── auth/            # Authentication pages
│   │   ├── departments/     # Department management
│   │   ├── courses/         # Course management
│   │   ├── fees/            # Fee management
│   │   └── dashboard/       # Dashboard pages
│   ├── hooks/               # Custom React hooks
│   ├── services/            # API service layer
│   ├── utils/               # Utility functions
│   ├── contexts/            # React contexts
│   ├── styles/              # Styling (CSS/SCSS)
│   ├── types/               # TypeScript type definitions
│   └── constants/           # Application constants
├── package.json             # Dependencies and scripts
└── README.md               # Frontend documentation
```

## 🎨 System Functionalities

### 1. Department Management Module
- **Create Departments**: Add new academic departments with unique codes
- **View Departments**: List and search through all departments
- **Update Departments**: Modify department information and descriptions  
- **Delete Departments**: Remove departments with proper validation
- **Department Codes**: Unique identifier system for departments

### 2. Course Management Module
- **Course Creation**: Add courses with codes, names, and credit hours
- **Course Modification**: Update course information and requirements
- **Credit Management**: Track and manage course credit hours
- **Course Upload System**: File upload capabilities for course materials
- **Course Search**: Advanced search and filtering capabilities

### 3. Fee Management System
- **Fee Type Management**: Create and manage different fee categories
- **Amount Configuration**: Set and adjust fee amounts dynamically
- **Department-Specific Fees**: Link fees to specific departments
- **Financial Tracking**: Monitor and report on fee structures

### 4. Academic Schedule Management
- **Schedule Creation**: Build comprehensive academic schedules
- **Time Management**: Manage class times and academic periods
- **Resource Allocation**: Schedule rooms and academic resources
- **Conflict Resolution**: Prevent scheduling conflicts automatically

### 5. User Authentication & Authorization
- **User Registration**: Secure user account creation
- **Login System**: JWT-based authentication with RSA encryption
- **Role Management**: Create and assign user roles dynamically
- **Permission Control**: Granular permission system
- **Password Security**: Secure password policies and reset functionality

### 6. Security & Monitoring
- **Rate Limiting**: Protect APIs from abuse and overload
- **Global Exception Handling**: Comprehensive error management
- **Audit Logging**: Track user actions and system events
- **Data Validation**: Input sanitization at all levels

## 🔧 System Design Principles

### 1. Clean Architecture
- **Separation of Concerns**: Clear layer separation and responsibilities
- **Dependency Inversion**: Abstract dependencies for testability
- **Domain-Driven Design**: Business logic at the core
- **Technology Independence**: Framework-agnostic domain layer

### 2. CQRS (Command Query Responsibility Segregation)
- **Command Handlers**: Separate write operations
- **Query Handlers**: Optimized read operations  
- **Event-Driven Architecture**: Loose coupling between components
- **Scalability**: Independent scaling of read/write operations

### 3. Security-First Approach
- **JWT with RSA Encryption**: Industry-standard security
- **Role-Based Access Control**: Granular permission management
- **Input Validation**: Prevent injection attacks
- **Rate Limiting**: Protection against abuse

### 4. Scalability & Performance
- **Modular Design**: Easy to extend and maintain
- **Database Optimization**: Efficient queries and indexing
- **Caching Strategies**: Performance optimization
- **API Design**: RESTful principles for clarity

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server 2019+ (LocalDB acceptable for development)
- Node.js 18+ and npm
- Visual Studio 2022 or VS Code

### Backend Setup
1. **Clone the repository**
   ```bash
   git clone [repository-url]
   cd AYA-UIS/AYA_UIS_Server
   ```

2. **Configure database connections**
   ```bash
   # Update appsettings.json with your SQL Server connection strings
   # InfoConnection: Main application database
   # IdentityConnection: User management database  
   ```

3. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Start the API**
   ```bash
   dotnet run --project AYA_UIS.API
   ```

### Frontend Setup
1. **Navigate to frontend directory**
   ```bash
   cd AYA-UIS/AYA_UIS_Frontend
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Start development server**
   ```bash
   npm start
   ```

## 📖 API Documentation

The API follows RESTful principles and includes:
- **Swagger Documentation**: Available at `/swagger` when running in development
- **Authentication Endpoints**: User registration, login, and token management
- **Department CRUD**: Complete department management
- **Course CRUD**: Full course management capabilities  
- **Fee Management**: Fee type and amount management
- **Schedule Management**: Academic schedule operations

## 🔮 Future Enhancements

### Phase 2 Development
- **Student Information System**: Grade management and student records
- **Faculty Management**: Faculty profiles and course assignments
- **Reporting Dashboard**: Advanced analytics and reporting
- **Mobile Application**: React Native mobile app
- **Real-time Notifications**: WebSocket-based notifications

### Technical Improvements
- **Microservices Architecture**: Service decomposition for scalability
- **Event Sourcing**: Complete audit trail with event storage
- **GraphQL API**: Alternative query interface
- **Container Deployment**: Docker and Kubernetes support

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Development Team

- **Backend Development**: .NET Core, Clean Architecture, CQRS
- **Frontend Development**: React, Modern UI/UX
- **Database Design**: SQL Server, Entity Framework
- **DevOps**: CI/CD, Deployment, Monitoring

## 📞 Support

For support, email [support@aya-uis.com] or create an issue in the repository.

---

*Built with ❤️ for modern university management*