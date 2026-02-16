# School Management System

A comprehensive school management system built with .NET, featuring a REST API, web client, and Blazor WebAssembly client. The system provides functionality for managing schools, students, employees, positions, rooms, and floors.

## 🏗️ Architecture

This solution follows a clean architecture pattern with the following projects:

### Core Projects
- **SchoolManagement.Models** (`school.Models/`) - Domain entities and interfaces
- **SchoolManagement.Data** (`school.Data/`) - Data access layer with Entity Framework Core

### Application Projects
- **SchoolManagement.API** (`SchoolManagement.API/`) - REST API built with ASP.NET Core Minimal APIs
- **SchoolManagement.Web** (`school.Web/`) - Web application using Razor Pages
- **SchoolManagement.Client** (`SchoolManagement.Client/`) - Blazor WebAssembly client application
- **SchoolManagement.Console** (`school.Console/`) - Console application for testing and utilities

## 🚀 Features

### Core Entities
- **Schools** - Manage school information including name, address, and opening date
- **Students** - Track student information with age restrictions (5-18 years)
- **Employees** - Manage employee records with age restrictions (18-65 years)
- **Positions** - Define employee positions within the school
- **Floors & Rooms** - Organize school infrastructure by floors and rooms
- **Addresses** - Store location information for schools

### Key Functionality
- CRUD operations for all entities
- Input validation with FluentValidation
- Pagination support for list views
- Swagger/OpenAPI documentation for the API
- Responsive web interface
- Docker support for containerized deployment

## 🛠️ Technology Stack

- **.NET 6+** - Core framework
- **ASP.NET Core** - Web framework
- **Entity Framework Core** - ORM for data access
- **FluentValidation** - Input validation
- **Blazor WebAssembly** - Client-side SPA framework
- **Razor Pages** - Server-side web pages
- **SQL Server** - Database
- **Docker** - Containerization
- **Swagger/OpenAPI** - API documentation

## 📋 Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download) or later
- [SQL Server](https://www.microsoft.com/sql-server) (or SQL Server Express/LocalDB)
- [Docker](https://www.docker.com/) (optional, for containerized deployment)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) (recommended)

## 🚦 Getting Started

### Option 1: Running with Docker Compose (Recommended)

The easiest way to run the entire application stack:

```bash
# Clone the repository
git clone https://github.com/vityok02/school.git
cd school

# Start all services
docker-compose up -d

# Access the applications:
# - API: http://localhost:5000 (HTTP) or https://localhost:5001 (HTTPS)
# - Client: http://localhost:7000 (HTTP) or https://localhost:7001 (HTTPS)
# - Database: localhost:1433
```

The docker-compose setup includes:
- SchoolManagement.API on ports 5000/5001
- SchoolManagement.Client on ports 7000/7001
- SQL Server 2019 on port 1433

**⚠️ Important**: The default SQL Server password in `docker-compose.yml` is for development only. Change it for production deployments.

### Option 2: Running Locally

1. **Clone the repository**
   ```bash
   git clone https://github.com/vityok02/school.git
   cd school
   ```

2. **Update connection strings** (if needed)
   
   Edit the connection string in:
   - `SchoolManagement.API/appsettings.Development.json`
   - `SchoolManagement.Web/appsettings.Development.json`

3. **Run database migrations**
   ```bash
   dotnet ef database update --project school.Data --startup-project SchoolManagement.API
   ```
   
   Note: The directory is `school.Data` but the project is `SchoolManagement.Data`

4. **Run the API**
   ```bash
   cd SchoolManagement.API
   dotnet run
   ```
   
   The API will be available at `http://localhost:5000` and `https://localhost:5001`
   
   Access Swagger UI at: `https://localhost:5001/swagger` or `http://localhost:5000/swagger`

5. **Run the Web Application** (in a new terminal)
   ```bash
   cd school.Web
   dotnet run
   ```

6. **Run the Blazor Client** (in a new terminal)
   ```bash
   cd SchoolManagement.Client
   dotnet run
   ```

## 🏗️ Building the Solution

```bash
# Build the entire solution
dotnet build

# Build in Release mode
dotnet build -c Release

# Run tests (if available)
dotnet test
```

## 📁 Project Structure

```
school/
├── SchoolManagement.API/          # REST API with Minimal APIs
│   ├── Features/                  # Feature-based organization
│   │   ├── Schools/               # School endpoints and handlers
│   │   ├── Students/              # Student endpoints and handlers
│   │   ├── Employees/             # Employee endpoints and handlers
│   │   ├── Positions/             # Position endpoints and handlers
│   │   ├── Floors/                # Floor endpoints and handlers
│   │   └── Rooms/                 # Room endpoints and handlers
│   └── Program.cs                 # API entry point
├── SchoolManagement.Client/       # Blazor WebAssembly client
│   ├── Features/                  # Feature components
│   └── wwwroot/                   # Static assets
├── SchoolManagement.Web/          # Razor Pages web application
│   └── Pages/                     # Razor pages
├── school.Models/                 # Domain models and interfaces
│   ├── Interfaces/                # Repository interfaces
│   ├── Student.cs
│   ├── Employee.cs
│   ├── School.cs
│   └── ...
├── school.Data/                   # Data access layer
│   ├── Repositories/              # Repository implementations
│   ├── Configurations/            # EF Core configurations
│   ├── Migrations/                # Database migrations
│   ├── AppDbContext.cs            # Database context
│   └── DataSeeder.cs              # Initial data seeding
├── school.Console/                # Console application
├── docker-compose.yml             # Docker composition
└── school.sln                     # Solution file
```

## 🔧 Configuration

### Database Connection

The default connection string in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SchoolDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

For Docker, the connection string is configured to use the SQL Server container:
```
Server=db;Database=SchoolDB;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=True
```

**⚠️ Security Note**: The default password `yourStrong(!)Password` is for development purposes only. Always change this password in production environments and use secure password management practices.

### API Base URL

When running the Blazor client, configure the API URL in `SchoolManagement.Client/appsettings.json`:

```json
{
  "ApiUrl": "http://localhost:5000"
}
```

## 🧪 Testing

The solution includes validation logic and business rules:
- Students must be between 5-18 years old
- Employees must be between 18-65 years old
- Duplicate students are prevented (same first name, last name, and age)
- Floor numbers must be unique within a school

## 🐳 Docker Support

### Building Docker Images

```bash
# Build API image
docker build -f SchoolManagement.API/Dockerfile -t schoolmanagement-api .

# Build Client image
docker build -f SchoolManagement.Client/Dockerfile -t schoolmanagement-client .
```

### Docker Compose Commands

```bash
# Start all services
docker-compose up -d

# Stop all services
docker-compose down

# View logs
docker-compose logs -f

# Rebuild and start
docker-compose up -d --build
```

## 📚 API Documentation

When running the API in development mode, Swagger documentation is available at:
- `http://localhost:5000/swagger`
- `https://localhost:5001/swagger`

The API follows RESTful conventions with endpoints for:
- `/api/schools` - School management
- `/api/students` - Student management
- `/api/employees` - Employee management
- `/api/positions` - Position management
- `/api/floors` - Floor management
- `/api/rooms` - Room management

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is available for educational and personal use.

## 📧 Contact

Project Link: [https://github.com/vityok02/school](https://github.com/vityok02/school)

---

Made with ❤️ using .NET
